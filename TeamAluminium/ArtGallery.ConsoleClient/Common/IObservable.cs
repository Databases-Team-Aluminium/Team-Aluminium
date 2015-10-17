namespace ArtGallery.ConsoleClient.Common
{
    using System.Collections.Generic;

    public interface IObservable
    {
        void Subscribe(IObserver observer);

        void Unsubscribe(IObserver observer);

        void ChangeState(Notification notification);

        void Notify();
    }
}