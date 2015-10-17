namespace ArtGallery.ConsoleClient
{
    using ArtGallery.ConsoleClient.Importers;
    using System;

    public class Startup
    {
        public static void Main()
        {
            var importer = new XmlImporter();

            new MongoDbDataImporter(importer).ImportSampleData(Console.Out);
        }
    }
}
