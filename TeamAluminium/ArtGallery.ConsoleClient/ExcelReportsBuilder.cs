namespace ArtGallery.ConsoleClient
{
    using System.Collections.Generic;
    using System.IO;

    using Models.MongoDbModels;
    using MongoDB.Driver;
    using System.Linq;
    using Models.Common;
    using System.Data.OleDb;
    using Utils;

    internal class ExcelReportsBuilder
    {
        private const string ExcelConnectionString = @"Provider=Microsoft.Jet.OleDB.4.0;" +
                @"Data Source=../../Data/SalesReports/{0}.xls; Persist Security Info=false;Extended Properties=Excel 8.0";
        private const string MongoDbConnetionString = "mongodb://localhost:27017";
        private const string DbName = "artgallerydb";

        private readonly TextWriter writer;

        public ExcelReportsBuilder(TextWriter tw)
        {
            this.writer = tw;
        }

        public void BuildAnnualReports()
        {
            RandomGenerator random = RandomGenerator.Create();
            this.writer.WriteLine(value: "Writing excel reports...");

            IEnumerable<ArtWork> soldArtworks = this.GatherSalesInformation();
            List<IGrouping<int, ArtWork>> groupedSoldArtworks = soldArtworks
                .GroupBy(a => a.DateSold.Year)
                .ToList();
            
            foreach (var group in groupedSoldArtworks)
            {
                var randomTableName = random.GetStringBetween(min: 0, max: 10);
                using (var db = new OleDbConnection(string.Format(ExcelConnectionString, group.Key + "-Sales-Report")))
                {
                    db.Open();
                    var createTableCmd = new OleDbCommand(string.Format(format: "CREATE TABLE [{0}](Title NVARCHAR(50), SoldFor MONEY)", arg0: randomTableName), connection: db);
                    createTableCmd.ExecuteNonQuery();

                    foreach (var item in group)
                    {
                        var insertCmd = new OleDbCommand(string.Format(format: "INSERT INTO [{0}](Title, SoldFor) VALUES('{1}', {2})", arg0: randomTableName, arg1: item.Title, arg2: item.Value), db);
                        insertCmd.ExecuteNonQuery();
                    }
                }
            }

            this.writer.WriteLine(value: "Done.");
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
    }
}