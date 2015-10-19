namespace LoadXmlIntoMongoDbAndMsSql.DataImporters
{
    using System.Collections.Generic;

    using Contracts;
    using Common;
    using DataLoaders.Contracts;
    using MongoDB.Driver;
    using Models;
    using System.Linq;

    public class MongoDbDataImporter : IDataImporter
    {
        private const string ConnetionString = "mongodb://localhost:27017";
        private const string DbName = "artgallerydb";

        private readonly IDataLoader dataLoader;
        private readonly ICollection<IObserver> subscribers;
        private Notification state;

        public MongoDbDataImporter(IDataLoader loader)
        {
            this.subscribers = new List<IObserver>();
            this.dataLoader = loader;
        }

        public void ImportData()
        {
            this.ChangeState(new Notification
            {
                Message = "Importing XML data into MongoDb..."
            });

            MongoDatabase db = this.GetDatabase(DbName);
            this.WriteDataToDb(db);

            this.ChangeState(new Notification
            {
                Message = "Done."
            });
        }

        public void ChangeState(Notification notification)
        {
            this.state = notification;
            this.Notify();
        }

        public void Notify()
        {
            foreach (var observer in this.subscribers)
            {
                observer.Update(this.state);
            }
        }

        public void Subscribe(IObserver observer)
        {
            this.subscribers.Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            this.subscribers.Remove(observer);
        }

        private MongoDatabase GetDatabase(string dbName)
        {
            var client = new MongoClient(ConnetionString);
            MongoServer server = client.GetServer();
            return server.GetDatabase(dbName);
        }

        private void WriteDataToDb(MongoDatabase db)
        {
            List<ArtWorkInformation> generatedArtWorksInfo = this
                .dataLoader
                .GetArtWorksInformation()
                .ToList();
            
            MongoCollection<ArtWorkInformation> artists = db
                .GetCollection<ArtWorkInformation>(collectionName: "artworksInformation");
            
            generatedArtWorksInfo.ForEach(i => artists.Save(i));
        }
    }
}
