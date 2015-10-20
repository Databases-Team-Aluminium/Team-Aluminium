namespace CreateJsonReports.JsonManagers.Contracts
{
    using Common;

    public interface IManager : IObservable
    {
        void WriteData();
    }
}
