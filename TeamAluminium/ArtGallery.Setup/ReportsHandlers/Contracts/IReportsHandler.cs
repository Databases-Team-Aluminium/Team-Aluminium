namespace ArtGallery.Setup.ReportsHandlers.Contracts
{
    using Common;

    public interface IReportsHandler : IObservable
    {
        void BuildReports();
    }
}
