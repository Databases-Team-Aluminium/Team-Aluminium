namespace ArtGallery.Setup.DataImporters.Contracts
{
    using Common;

    public interface IDataImporter : IObservable
    {
        void ImportData();
    }
}
