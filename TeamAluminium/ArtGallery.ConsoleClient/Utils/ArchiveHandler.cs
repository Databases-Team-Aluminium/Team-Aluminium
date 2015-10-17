namespace ArtGallery.ConsoleClient
{
    using Common;
    using System.Collections.Generic;
    using System.IO.Compression;

    public class ArchiveHandler : IObservable
    {
        private ICollection<IObserver> subscribers;
        private Notification state;

        public ArchiveHandler()
        {
            this.subscribers = new List<IObserver>();
        }

        public void ZipFolder(string source, string destination)
        {
            this.ChangeState(new Notification
            {
                Message = "Zipping files..."
            });

            ZipFile.CreateFromDirectory(source, destination);

            this.ChangeState(new Notification
            {
                Message = "Done."
            });
        }

        public void UnzipToFolder(string source, string destination)
        {
            this.ChangeState(new Notification
            {
                Message = "Unzipping files..."
            });

            ZipFile.CreateFromDirectory(source, destination);

            this.ChangeState(new Notification
            {
                Message = "Done."
            });
        }

        public void ChangeState(Notification notification)
        {
            this.state = notification;
            this.Notify();
        }

        public void Subscribe(IObserver observer)
        {
            this.subscribers.Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            this.subscribers.Remove(observer);
        }

        public void Notify()
        {
            foreach (var subscriber in this.subscribers)
            {
                subscriber.Update(this.state);
            }
        }
    }
}
