namespace ArtGallery.ConsoleClient
{
    using System;

    public class Startup
    {
        public static void Main()
        {
            Console.WriteLine(value: "Importing data into artgallerydb...");

            var consoleImporter = new DataImporter();
            consoleImporter.ImportSampleData();

            Console.WriteLine(value: "Done.");
        }
    }
}
