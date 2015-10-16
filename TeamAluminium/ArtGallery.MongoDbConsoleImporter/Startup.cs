namespace ArtGallery.MongoDbConsoleImporter
{
    using System;
    using System.Collections.Generic;

    using Models;
    using MongoDB.Driver;

    public class Startup
    {
        /// TODO: FIX MODELS CONSTRUCTORS

        public static void Main()
        {
            Console.WriteLine(value: "Importing data into artgallerydb...");

            ImportSampleData();

            Console.WriteLine(value: "Done.");
        }

        private static void ImportSampleData()
        {
            string connectionString = "mongodb://localhost:27017";
            var client = new MongoClient(connectionString);
            MongoServer server = client.GetServer();
            MongoDatabase db = server.GetDatabase(databaseName: "artgallerydb");

            var sampleCountry = new Country();
            sampleCountry.Id = 1;
            sampleCountry.Name = "BULGARIA";
            sampleCountry.Population = new List<Person>
            {
                new Person()
            };

            MongoCollection<Country> collection = db
                .GetCollection<Country>(collectionName: "countries");
            collection.Save(sampleCountry);
        }
    }
}
