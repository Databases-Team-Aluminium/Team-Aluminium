namespace ArtGallery.ConsoleClient
{
    using Writers;
    using DataImporters;
    using ReportsHandlers;
    using System;
    using ArtGallery.ConsoleClient.Importers;

    public class Client
    {
        private const string PathToAnnualReportsArchive = @"../../Data/SalesReports.zip";
        private const string PathToAnnualReports = @"../../Data/SalesReports";

        private static Client instance;

        private Client()
        {
        }

        public static Client Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Client();
                }

                return instance;
            }
        }

        public void SetupDb()
        {
            var txtFileimporter = new TxtFileImporter();
            var consoleWriter = new TextWriter(Console.Out);
            var importer = new MongoDbDataImporter(txtFileimporter);
            var builder = new ExcelAnnualSalesReportsHandler();
            var archiver = new ArchiveHandler();

            importer.Subscribe(consoleWriter);
            builder.Subscribe(consoleWriter);
            archiver.Subscribe(consoleWriter);

            importer.ImportData();
            builder.BuildReports();
            archiver.ZipFolder(PathToAnnualReports, PathToAnnualReportsArchive);
        }
    }
}
