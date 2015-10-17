namespace ArtGallery.ConsoleClient.ReportsHandlers
{
    using System.Collections.Generic;
    using System.Data.OleDb;
    using System.Linq;

    using Common;
    using Contracts;
    using Models.Common;
    using Models.Exhibits;
    using MongoDB.Driver;
    using Utils;

    public class ExcelAnnualSalesReportsHandler : IReportsHandler, IObservable
    {
        private const string ExcelConnectionString = @"Provider=Microsoft.Jet.OleDB.4.0;" +
                @"Data Source=../../Data/SalesReports/{0}.xls; Persist Security Info=false;Extended Properties=Excel 8.0";
        private const string MongoDbConnetionString = "mongodb://localhost:27017";
        private const string DbName = "artgallerydb";

        private ICollection<IObserver> subscribers;
        private Notification state;

        public ExcelAnnualSalesReportsHandler()
        {
            this.subscribers = new List<IObserver>();
        }
        
        public void BuildReports()
        {
            this.ChangeState(new Notification
            {
                Message = "Creating reports..."
            });

            RandomGenerator random = RandomGenerator.Create();

            IEnumerable<ArtWork> soldArtworks = this.GatherSalesInformation();
            List<IGrouping<int, ArtWork>> groupedSoldArtworks = soldArtworks
                .GroupBy(a => a.DateSold.Year)
                .ToList();

            foreach (var group in groupedSoldArtworks)
            {
                string randomTableName = random.GetStringBetween(min: 0, max: 10);
                using (var db = new OleDbConnection(string.Format(ExcelConnectionString, group.Key + "-Sales-Report")))
                {
                    db.Open();
                    var createTableCmd = new OleDbCommand();
                    createTableCmd.CommandText = string.Format(format: "CREATE TABLE [{0}] (Title NVARCHAR(200), SoldFor MONEY)", arg0: randomTableName);
                    createTableCmd.Connection = db;
                    createTableCmd.ExecuteNonQuery();

                    foreach (var item in group)
                    {
                        var insertCmd = new OleDbCommand(string.Format(format: "INSERT INTO [{0}] (Title, SoldFor) VALUES('{1}', {2})", arg0: randomTableName, arg1: item.Title, arg2: item.Value), db);
                        insertCmd.ExecuteNonQuery();
                    }
                }
            }

            this.ChangeState(new Notification
            {
                Message = "Done."
            });
        }

        private IEnumerable<ArtWork> GatherSalesInformation()
        {
            var client = new MongoClient(MongoDbConnetionString);
            MongoServer server = client.GetServer();
            MongoDatabase db = server.GetDatabase(DbName);

            IEnumerable<ArtWork> soldArtWorks = db
                .GetCollection<ArtWork>(collectionName: "artworks")
                .FindAll()
                .Where(a => a.Status == ArtWorkStatus.Sold);

            return soldArtWorks;
        }

        public void Subscribe(IObserver observer)
        {
            this.subscribers.Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            this.subscribers.Remove(observer);
        }

        public void ChangeState(Notification notification)
        {
            this.state = notification;
            this.Notify();
        }

        public void Notify()
        {
            foreach (var subscriber in this.subscribers)
            {
                subscriber.Update(this.state);
            }
        }
    }
}