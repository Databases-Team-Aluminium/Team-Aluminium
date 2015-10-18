namespace LoadXmlIntoMongoDbAndMsSql.Common
{
    public interface IObservable
    {
        void Subscribe(IObserver observer);

        void Unsubscribe(IObserver observer);

        void ChangeState(Notification notification);

        void Notify();
    }
}