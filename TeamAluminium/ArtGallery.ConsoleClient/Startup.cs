namespace ArtGallery.ConsoleClient
{
    using ArtGallery.ConsoleClient.Importers;
    using System;

    public class Startup
    {
        public static void Main()
        {
            Client.Instance.SetupDb();
        }
    }
}
