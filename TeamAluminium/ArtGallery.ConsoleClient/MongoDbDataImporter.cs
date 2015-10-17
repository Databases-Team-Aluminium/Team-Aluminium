namespace ArtGallery.ConsoleClient
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Utils;

    using ArtGallery.ConsoleClient.Importers;
    using Models.MongoDbModels;
    using MongoDB.Driver;

    public class MongoDbDataImporter
    {
        private IImporter importer;

        public MongoDbDataImporter(IImporter importer)
        {
            this.importer = importer;
        }


        public void ImportSampleData(TextWriter tw)
        {
            string connectionString = "mongodb://localhost:27017";
            var client = new MongoClient(connectionString);
            MongoServer server = client.GetServer();
            MongoDatabase db = server.GetDatabase(databaseName: "artgallerydb");

            List<Artist> generatedArtists = this.importer.GetArtists().ToList();
            List<ArtWork> generatedArtworks = this.importer.GetArtworks().ToList();
            
            RandomGenerator random = RandomGenerator.Create();

            // Generate relations between artists and artworks
            foreach (var item in generatedArtworks)
            {
                Artist randomArtist = generatedArtists[random
                    .GetIntegerBetween(0, generatedArtists.Count - 1)];
                item.ArtistId = randomArtist.Id;
            }


            MongoCollection<Artist> artists = db
                .GetCollection<Artist>(collectionName: "artists");

            MongoCollection<ArtWork> artworks = db
                .GetCollection<ArtWork>(collectionName: "artworks");

            // Write artists to db
            foreach (var item in generatedArtists)
            {
                artists.Save(item);
            }

            // Write artworks to db
            foreach (var item in generatedArtworks)
            {
                artworks.Save(item);
            }
        }
    }
}
