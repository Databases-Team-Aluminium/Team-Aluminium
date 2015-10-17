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
        private const int NumberOfArtists = 100;

        public void ImportSampleData(TextWriter tw)
        {
            string connectionString = "mongodb://localhost:27017";
            var client = new MongoClient(connectionString);
            MongoServer server = client.GetServer();
            MongoDatabase db = server.GetDatabase(databaseName: "artgallerydb");
            
            List<Artist> generatedArtists = this.GetArtists().ToList();
            List<ArtWork> generatedArtworks = this.GetArtworks().ToList();
            
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

        private ICollection<Artist> GetArtists()
        {
            RandomGenerator random = RandomGenerator.Create();
            var firstNames = File
                .ReadAllText(path: "../../Data/PeopleNames/FirstNames.txt")
                .Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Take(NumberOfArtists)
                .ToList();

            var lastNames = File
                .ReadAllText(path: "../../Data/PeopleNames/LastNames.txt")
                .Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Take(NumberOfArtists)
                .ToList();

            var artists = new HashSet<Artist>();
            for (int i = 0; i < NumberOfArtists; i++)
            {
                var artist = new Artist
                {
                    Id = i + 1,
                    FirstName = firstNames[i],
                    MiddleName = UpperCaseLetters
                        [random
                        .GetIntegerBetween(min: 0, max: UpperCaseLetters.Length - 1)]
                        .ToString(),
                    LastName = lastNames[i],
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
                    Title = names[i],
                    Type = (ArtWorkType)random.GetIntegerBetween(0, 9),
                    Status = ArtWorkStatus.ForExhibition
                });
            }

            return artWorks;
        }
    }
}
