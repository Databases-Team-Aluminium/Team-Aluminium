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
        private const string ConnetionString = "mongodb://localhost:27017";
        private const string DbName = "artgallerydb";
        private const string UpperCaseLetters = "ABCDEFGHIJKLMNOPQRTUVWXYZ";
        private const int NumberOfArtists = 100;
        private const int NumberOfEmployees = 50;

        public void ImportSampleData(TextWriter tw)
        {
            MongoDatabase db = this.GetDatabase();
            tw.WriteLine(value: "Writing data...");
            this.WriteDataToDb(db);
            tw.WriteLine(value: "Done.");
        }

        private MongoDatabase GetDatabase()
        {
            var client = new MongoClient(ConnetionString);
            MongoServer server = client.GetServer();
            return server.GetDatabase(DbName);
        }

        private void WriteDataToDb(MongoDatabase db)
        {
            List<Artist> generatedArtists = this.GetArtists().ToList();
            List<ArtWork> generatedArtworks = this.GetArtworks().ToList();
            List<Country> generatedCountries = this.GetCountries().ToList();
            List<Department> generatedDepartments = this.GetDepartments().ToList();
            List<Employee> generatedEmployees = this.GetEmployees().ToList();

            RandomGenerator random = RandomGenerator.Create();

            // Generate relations between artists and artworks
            foreach (var item in generatedArtworks)
            {
                Artist randomArtist = generatedArtists[random
                    .GetIntegerBetween(0, generatedArtists.Count - 1)];
                item.ArtistId = randomArtist.Id;
            }

            // Generate relations between artists and countries
            foreach (var item in generatedArtists)
            {
                Country randomCountry = generatedCountries[random.GetIntegerBetween(0, generatedCountries.Count - 1)];
                item.CountryId = randomCountry.Id;
            }

            // Generate relations between employees and countries
            foreach (var item in generatedEmployees)
            {
                Country randomCountry = generatedCountries[random.GetIntegerBetween(0, generatedCountries.Count - 1)];
                item.CountryId = randomCountry.Id;
            }

            // Generate relations between employees and departments
            foreach (var item in generatedEmployees)
            {
                Department randomDepartment = generatedDepartments[random.GetIntegerBetween(0, generatedDepartments.Count - 1)];
                item.DepartmentId = randomDepartment.Id;
            }

            MongoCollection<Artist> artists = db
                .GetCollection<Artist>(collectionName: "artists");

            MongoCollection<ArtWork> artworks = db
                .GetCollection<ArtWork>(collectionName: "artworks");

            MongoCollection<Country> countries = db
                .GetCollection<Country>(collectionName: "countries");

            MongoCollection<Department> departments = db
                .GetCollection<Department>(collectionName: "departments");

            MongoCollection<Employee> employees = db
                .GetCollection<Employee>(collectionName: "employees");

            // Write departments to db
            generatedDepartments.ForEach(d => departments.Save(d));

            // Write countries to db
            generatedCountries.ForEach(c => countries.Save(c));

            // Write artists to db
            generatedArtists.ForEach(a => artists.Save(a));

            // Write artworks to db
            generatedArtworks.ForEach(aw => artworks.Save(aw));

            // Write employees to db
            generatedEmployees.ForEach(e => employees.Save(e));
        }
        
        private ICollection<Artist> GetArtists()
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

        private ICollection<Country> GetCountries()
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

        private ICollection<Department> GetDepartments()
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

        private ICollection<Employee> GetEmployees()
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
