namespace ArtGallery.ConsoleClient.DataImporters.Contracts
{
    using Common;

    public interface IDataImporter : IObservable
    {
        void ImportData();
    }
}
