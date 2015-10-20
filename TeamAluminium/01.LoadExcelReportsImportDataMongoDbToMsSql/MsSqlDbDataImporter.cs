namespace LoadExcelReportsImportDataMongoDbToMsSql
{
    using System.Collections.Generic;
    using System.Linq;

    using ArtGallery.SqlServerData;
    using ArtGallery.Setup.Common;
    using ArtGallery.SqlServerModels.Exhibits;
    using ArtGallery.SqlServerModels.People;
    using ArtGallery.SqlServerModels.Places;
    using Omu.ValueInjecter;

    public class MsSqlDbDataImporter : IObservable
    {
        private IDataProvider dataProvider;
        private ICollection<IObserver> subscribers;
        private Notification state;
        private ArtGalleryDbContext data;

        public MsSqlDbDataImporter(IDataProvider dataProvider, ArtGalleryDbContext data)
        {
            this.dataProvider = dataProvider;
            this.subscribers = new List<IObserver>();
            this.data = data;
        }

        public void ImportData()
        {
            this.ChangeState(new Notification
            {
                Message = "Transfering data from MongoDb into SQL Server..."
            });

            this.WriteDataToDb();

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

        private void WriteDataToDb()
        {
            var artists = this.dataProvider.GetArtists();
            var artWorks = this.dataProvider.GetArtWorks();
            var countries = this.dataProvider.GetCountries();

            var sqlCountries = countries.Select(x => (Country)new Country().InjectFrom(x)).ToList();

            var sqlArtWorks = artWorks.Select(x => (ArtWork)new ArtWork().InjectFrom(x)).ToList();

            foreach (var artist in artists)
            {
                var sqlArtist = new Artist();
                sqlArtist.InjectFrom(artist);

                sqlArtist.ArtWorks = sqlArtWorks.Where(x => x.ArtistId == sqlArtist.Id).ToList();
                sqlArtist.Country = sqlCountries.FirstOrDefault(x => x.Id == sqlArtist.CountryId);

                this.data.Artists.Add(sqlArtist);
            }

            this.data.SaveChanges();
        }
    }
}
