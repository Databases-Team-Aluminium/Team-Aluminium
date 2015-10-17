namespace ArtGallery.ConsoleClient.Writers
{
    using Common;
    using Contracts;

    public class TextWriter : IWriter, IObserver
    {
        private readonly System.IO.TextWriter writer;

        public TextWriter(System.IO.TextWriter writer)
        {
            this.writer = writer;
        }

        public void Write(string text)
        {
            this.writer.Write(text);
        }
        
        public void WriteLine(string text)
        {
            this.writer.WriteLine(text);
        }

        public void Update(Notification notification)
        {
            this.WriteLine(notification.Message);
        }
    }
}
