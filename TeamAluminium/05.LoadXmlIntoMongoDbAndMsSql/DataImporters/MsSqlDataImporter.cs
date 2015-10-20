namespace LoadXmlIntoMongoDbAndMsSql.DataImporters
{
    using System.Collections.Generic;

    using ArtGallery.SqlServerData;
    using Common;
    using Contracts;
    using DataLoaders.Contracts;
    using Omu.ValueInjecter;
    using System.Linq;
    using ArtGallery.SqlServerModels.Additional;

    public class MsSqlDataImporter : IDataImporter
    {
        private readonly ICollection<IObserver> subscribers;
        private Notification state;
        private readonly IDataLoader dataLoader;

        public MsSqlDataImporter(IDataLoader loader)
        {
            this.dataLoader = loader;
            this.subscribers = new List<IObserver>();
        }

        public void ChangeState(Notification notification)
        {
            this.state = notification;
            this.Notify();
        }

        public void ImportData()
        {
            this.ChangeState(new Notification
            {
                Message = "Importing XML data into SQL Server.."
            });

            using (var db = new ArtGalleryDbContext())
            {
                this
                .dataLoader
                .GetArtWorksInformation()
                .Select(x => (ArtWorkDescription)new ArtWorkDescription().InjectFrom(x))
                .ToList()
                .ForEach(x => db.ArtWorksDescriptions.Add(x));

                db.SaveChanges();
            }

            this.ChangeState(new Notification
            {
                Message = "Done."
            });
        }

        public void Notify()
        {
            foreach (var subscriber in this.subscribers)
            {
                subscriber.Update(this.state);
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
    }
}
