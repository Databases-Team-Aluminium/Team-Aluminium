namespace ArtGallery.ConsoleClient
{
    public class Startup
    {
        public static void Main()
        {
            Client.Instance.SetupDb();
        }
    }
}
