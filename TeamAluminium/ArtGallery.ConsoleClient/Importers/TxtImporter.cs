namespace ArtGallery.ConsoleClient.Importers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using ArtGallery.ConsoleClient.Utils;
    using ArtGallery.Models.Common;
    using ArtGallery.Models.MongoDbModels;

    public class TxtImporter : IImporter
    {
        private const string UpperCaseLetters = "ABCDEFGHIJKLMNOPQRTUVWXYZ";
        private const int NumberOfArtists = 100;

        public ICollection<Artist> GetArtists()
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

        public ICollection<ArtWork> GetArtworks()
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
