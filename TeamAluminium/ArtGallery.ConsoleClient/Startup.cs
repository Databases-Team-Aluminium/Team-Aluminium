namespace ArtGallery.ConsoleClient
{
    using ArtGallery.ConsoleClient.Importers;
    using System;

    public class Startup
    {
        public static void Main()
        {
            var importer = new TxtImporter();

            new MongoDbDataImporter(importer).ImportSampleData(Console.Out);
        }
    }
}
