namespace ArtGallery.ConsoleClient
{
    using System;

    public class Startup
    {
        public static void Main()
        {
            var importer = new MongoDbDataImporter();
            importer.ImportSampleData(Console.Out);

            var builder = new ExcelReportsBuilder(Console.Out);
            builder.BuildAnnualReports();
        }
    }
}
