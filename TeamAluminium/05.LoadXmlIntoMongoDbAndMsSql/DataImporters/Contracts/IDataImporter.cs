namespace LoadXmlIntoMongoDbAndMsSql.DataImporters.Contracts
{
    using Common;

    public interface IDataImporter : IObservable
    {
        void ImportData();
    }
}
