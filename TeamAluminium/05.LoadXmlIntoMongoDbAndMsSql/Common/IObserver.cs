namespace LoadXmlIntoMongoDbAndMsSql.Common
{
    public interface IObserver
    {
        void Update(Notification notification);
    }
}