namespace ArtGallery.ConsoleClient.Importers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using ArtGallery.ConsoleClient.Utils;
    using ArtGallery.Models.People;
    using ArtGallery.Models.Exhibits;
    using ArtGallery.Models.Common;
    using ArtGallery.Models.Places;
    using ArtGallery.Models.Structures;


    public class TxtFileImporter : IImporter
    {
        private const string UpperCaseLetters = "ABCDEFGHIJKLMNOPQRTUVWXYZ";
        private const int NumberOfArtists = 100;
        private const int NumberOfEmployees = 50;

        public ICollection<Artist> GetArtists()
        {
            RandomGenerator random = RandomGenerator.Create();
            List<string> firstNames = File
                .ReadAllText(path: "../../Data/PeopleNames/FirstNames.txt")
                .Split(new char[] { '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries)
                .Take(NumberOfArtists)
                .ToList();

            List<string> lastNames = File
                .ReadAllText(path: "../../Data/PeopleNames/LastNames.txt")
                .Split(new char[] { '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries)
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
                    DateOfBirth = random.GetRandomDate(),
                    Style = (ArtStyle)random.GetIntegerBetween(0, 4)
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
                var artwork = new ArtWork
                {
                    Id = i + 1,
                    Title = names[i],
                    Type = (ArtWorkType)random.GetIntegerBetween(min: 0, max: 9),
                    Status = (ArtWorkStatus)random.GetIntegerBetween(min: 0, max: 3),
                    Value = random.GetIntegerBetween(min: 10000, max: 10000000)
                };

                if (artwork.Status == ArtWorkStatus.Sold)
                {
                    artwork.DateSold = random
                        .GetRandomDate(
                        start: new DateTime(year: 2010, month: 01, day: 01, hour: 00, minute: 00, second: 00),
                        end: new DateTime(year: 2014, month: 12, day: 31, hour: 23, minute: 59, second: 59));
                }

                artWorks.Add(artwork);
            }

            return artWorks;
        }

        public ICollection<Country> GetCountries()
        {
            string[] countryNames = File
                .ReadAllText(path: "../../Data/CountryNames.txt")
                .Split(new char[] { '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries);

            var countries = new List<Country>();

            for (var i = 0; i < countryNames.Length; i++)
            {
                countries.Add(new Country
                {
                    Id = i + 1,
                    Name = countryNames[i]
                });
            }

            return countries;
        }

        public ICollection<Department> GetDepartments()
        {
            List<string> departmentNames = File
                .ReadAllText(path: "../../Data/DepartmentNames.txt")
                .Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
                .ToList();

            var departments = new List<Department>();
            for (int i = 0; i < departmentNames.Count; i++)
            {
                departments.Add(new Department
                {
                    Id = i + 1,
                    Name = departmentNames[i]
                });
            }

            return departments;
        }

        public ICollection<Employee> GetEmployees()
        {
            RandomGenerator random = RandomGenerator.Create();
            List<string> firstNames = File
                .ReadAllText(path: "../../Data/PeopleNames/FirstNames.txt")
                .Split(new char[] { '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries)
                .Skip(NumberOfArtists)
                .Take(NumberOfEmployees)
                .ToList();

            List<string> lastNames = File
                .ReadAllText(path: "../../Data/PeopleNames/LastNames.txt")
                .Split(new char[] { '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries)
                .Skip(NumberOfArtists)
                .Take(NumberOfEmployees)
                .ToList();

            var employees = new HashSet<Employee>();
            for (int i = 0; i < NumberOfEmployees; i++)
            {
                var employee = new Employee
                {
                    Id = i + 1,
                    EmployeeIdentifier = random.GetStringBetween(min: 12, max: 15),
                    FirstName = firstNames[i],
                    MiddleName = UpperCaseLetters
                        [random
                        .GetIntegerBetween(min: 0, max: UpperCaseLetters.Length - 1)]
                        .ToString(),
                    LastName = lastNames[i],
                    DateOfBirth = random.GetRandomDate(),
                    YearSalary = random.GetIntegerBetween(min: 50000, max: 250000)
                };

                employees.Add(employee);
            }

            return employees;
        }
    }
}
