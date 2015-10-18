namespace LoadExcelReportsImportDataMongoDbToMsSql
{
    using System.Collections.Generic;
    using System.Linq;

    using ArtGallery.Models.Exhibits;
    using ArtGallery.Models.People;
    using ArtGallery.Models.Places;
    using MongoDB.Driver;
   
    public class MongoDb : IDataProvider
    {
        private const string ConnetionString = "mongodb://localhost:27017";
        private const string DbName = "artgallerydb";

        private MongoDatabase GetDatabase()
        {
            var client = new MongoClient(ConnetionString);
            MongoServer server = client.GetServer();
            return server.GetDatabase(DbName);
        }

        public ICollection<Artist> GetArtists()
        {
            return this.GetDatabase()
                .GetCollection<Artist>("artists")
                .FindAll()
                .ToList();
        }

        public ICollection<ArtWork> GetArtWorks()
        {
            return this.GetDatabase()
                .GetCollection<ArtWork>("artworks")
                .FindAll()
                .ToList();

        }

        public ICollection<Country> GetCountries()
        {
            return this.GetDatabase()
                .GetCollection<Country>("countries")
                .FindAll()
                .ToList();

        }
    }
}
