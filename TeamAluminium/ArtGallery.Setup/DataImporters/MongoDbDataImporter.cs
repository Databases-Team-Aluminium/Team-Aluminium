namespace ArtGallery.Setup.DataImporters
{
    using System.Collections.Generic;
    using System.Linq;

    using Common;
    using Contracts;
    using MongoDbModels.Exhibits;
    using MongoDbModels.People;
    using MongoDbModels.Places;
    using MongoDbModels.Structures;
    using MongoDB.Driver;
    using Utils;
    using DataLoaders.Contracts;

    public class MongoDbDataImporter : IDataImporter, IObservable
    {
        private const string ConnetionString = "mongodb://localhost:27017";
        private const string DbName = "artgallerydb";


        private readonly ICollection<IObserver> subscribers;
        private readonly IDataLoader dataLoader;
        private Notification state;

        public MongoDbDataImporter(IDataLoader loader)
        {
            this.dataLoader = loader;
            this.subscribers = new List<IObserver>();
        }

        public void ImportData()
        {
            this.ChangeState(new Notification
            {
                Message = "Importing data into MongoDb..."
            });

            MongoDatabase db = this.GetDatabase(DbName);
            this.WriteDataToDb(db);

            this.ChangeState(new Notification
            {
                Message = "Done."
            });
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
            foreach (IObserver subscriber in this.subscribers)
            {
                subscriber.Update(this.state);
            }
        }

        private MongoDatabase GetDatabase(string dbName)
        {
            var client = new MongoClient(ConnetionString);
            MongoServer server = client.GetServer();
            return server.GetDatabase(dbName);
        }

        private void WriteDataToDb(MongoDatabase db)
        {
            List<Artist> generatedArtists = this.dataLoader.GetArtists().ToList();
            List<ArtWork> generatedArtworks = this.dataLoader.GetArtworks().ToList();
            List<Country> generatedCountries = this.dataLoader.GetCountries().ToList();
            List<Department> generatedDepartments = this.dataLoader.GetDepartments().ToList();
            List<Employee> generatedEmployees = this.dataLoader.GetEmployees().ToList();

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
    }
}
