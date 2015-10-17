namespace ArtGallery.ConsoleClient.ReportsHandlers.Contracts
{
    using Common;

    public interface IReportsHandler : IObservable
    {
        void BuildReports();
    }
}
