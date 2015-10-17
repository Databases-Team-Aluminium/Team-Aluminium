namespace ArtGallery.ConsoleClient
{
    using System;

    public class Startup
    {
        public static void Main()
        {
            new MongoDbDataImporter().ImportSampleData(Console.Out);
        }
    }
}
