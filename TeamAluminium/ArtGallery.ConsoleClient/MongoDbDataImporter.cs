namespace ArtGallery.ConsoleClient
{
    using System.Collections.Generic;

    using Models.MongoDbModels;
    using MongoDB.Driver;
    using Utils;
    using System.IO;
    using System;
    using System.Linq;
    using Models.Common;

    public class MongoDbDataImporter
    {
        private const string UpperCaseLetters = "ABCDEFGHIJKLMNOPQRTUVWXYZ";

        public void ImportSampleData()
        {
            string connectionString = "mongodb://localhost:27017";
            var client = new MongoClient(connectionString);
            MongoServer server = client.GetServer();
            MongoDatabase db = server.GetDatabase(databaseName: "artgallerydb");

            MongoCollection<Artist> artists = db
                .GetCollection<Artist>(collectionName: "artists");

            foreach (var item in this.GetArtists())
            {
                artists.Save(item);
            }

            MongoCollection<ArtWork> artworks = db
                .GetCollection<ArtWork>(collectionName: "artworks");
            foreach (var item in this.GetArtworks())
            {
                artworks.Save(item);
            }
        }

        private ICollection<Artist> GetArtists()
        {
            RandomGenerator random = RandomGenerator.Create();
            var firstNames = File
                .ReadAllText(path: "../../Data/PeopleNames/FirstNames.txt")
                .Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .ToList();

            var lastNames = File
                .ReadAllText(path: "../../Data/PeopleNames/LastNames.txt")
                .Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .ToList();

            var artists = new HashSet<Artist>();
            for (int i = 0; i < 100; i++)
            {
                var artist = new Artist
                {
                    Id = i + 1,
                    FirstName = firstNames[random.GetIntegerBetween(0, firstNames.Count - 1)],
                    MiddleName = UpperCaseLetters
                        [random
                        .GetIntegerBetween(0, UpperCaseLetters.Length - 1)]
                        .ToString(),
                    LastName = lastNames[random.GetIntegerBetween(0, lastNames.Count - 1)],
                    DateOfBirth = random.GetRandomDate()
                };

                artists.Add(artist);
            }

            return artists;
        }

        private ICollection<ArtWork> GetArtworks()
        {
            RandomGenerator random = RandomGenerator.Create();
            List<string> names = File
                .ReadAllText(path: "../../Data/ArtWorkNames.txt")
                .Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .ToList();

            var artWorks = new HashSet<ArtWork>();
            for (int i = 0; i < 100; i++)
            {
                artWorks.Add(new ArtWork
                {
                    Id = i + 1,
                    Title = names[random.GetIntegerBetween(0, names.Count - 1)],
                    Type = (ArtWorkType)random.GetIntegerBetween(0, 9),
                    Status = ArtWorkStatus.ForExhibition
                });
            }

            return artWorks;
        }
    }
}
