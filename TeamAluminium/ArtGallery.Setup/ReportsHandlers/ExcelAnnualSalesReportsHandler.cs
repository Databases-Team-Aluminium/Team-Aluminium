namespace ArtGallery.Setup.ReportsHandlers
{
    using System.IO;
    using System.Collections.Generic;
    using System.Data.OleDb;
    using System.Linq;

    using Common;
    using Contracts;
    using MongoDbModels.Common;
    using MongoDbModels.Exhibits;
    using MongoDB.Driver;
    using Utils;

    public class ExcelAnnualSalesReportsHandler : IReportsHandler, IObservable
    {
        private const string PathToSalesReportsBaseFolder = @"../../../Output/SalesReports";
        private const string ExcelConnectionString = @"Provider=Microsoft.Jet.OleDB.4.0;" +
                @"Data Source=" + PathToSalesReportsBaseFolder + "/{1}-{2}-{0}/{0}-Sales-Report.xls; Persist Security Info=false;Extended Properties=Excel 8.0";
        private const string MongoDbConnetionString = "mongodb://localhost:27017";
        private const string DbName = "artgallerydb";
        private readonly string[] monthNames = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };

        private readonly ICollection<IObserver> subscribers;
        private Notification state;

        public ExcelAnnualSalesReportsHandler()
        {
            this.subscribers = new List<IObserver>();
        }

        public void BuildReports()
        {
            this.ChangeState(new Notification
            {
                Message = "Creating Excel reports..."
            });

            RandomGenerator random = RandomGenerator.Create();

            IEnumerable<ArtWork> soldArtworks = this.GatherSalesInformation();
            List<IGrouping<int, ArtWork>> groupedSoldArtworks = soldArtworks
                .Where(a => a.DateSold != null)
                .GroupBy(a => a.DateSold.Value.Year)
                .ToList();

            foreach (var group in groupedSoldArtworks)
            {
                string month = monthNames[(group.Key % 1937) % 12];
                string day = ((group.Key % 1937) % 29).ToString();

                if (day == "0")
                {
                    day = "01";
                }

                if (day.Length < 2)
                {
                    day = "0" + day;
                }

                string groupPath = PathToSalesReportsBaseFolder +
                    string.Format(format: "/{1}-{2}-{0}", arg0: group.Key, arg1: day, arg2: month);
                Directory.CreateDirectory(groupPath);
                string randomTableName = random.GetStringBetween(min: 0, max: 10);
                using (var db = new OleDbConnection(string.Format(ExcelConnectionString, group.Key, day, month)))
                {
                    db.Open();
                    var createTableCmd = new OleDbCommand();
                    createTableCmd.CommandText = "CREATE TABLE [@name] (Title NVARCHAR(200), SoldFor MONEY)";
                    createTableCmd.Parameters.AddWithValue
                        (parameterName: "@name", value: randomTableName);
                    createTableCmd.Connection = db;
                    createTableCmd.ExecuteNonQuery();

                    foreach (var item in group)
                    {
                        var insertCmd = new OleDbCommand(string.Format(format: "INSERT INTO [@name] (Title, SoldFor) VALUES('{1}', {2})", arg0: randomTableName, arg1: item.Title, arg2: item.Value), db);
                        insertCmd.Parameters.AddWithValue
                        (parameterName: "@name", value: randomTableName);
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