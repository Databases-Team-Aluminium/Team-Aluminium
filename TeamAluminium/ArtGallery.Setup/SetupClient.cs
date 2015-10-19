namespace ArtGallery.Setup
{
    using Writers;
    using DataImporters;
    using ReportsHandlers;
    using System;
    using DataLoaders;

    public class SetupClient
    {
        private const string PathToAnnualReportsArchive = @"../../../Output/SalesReports.zip";
        private const string PathToAnnualReports = @"../../../Output/SalesReports";

        private static SetupClient instance;

        private SetupClient()
        {
        }

        public static SetupClient Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SetupClient();
                }

                return instance;
            }
        }

        public void Run()
        {
            var textFileLoader = new TextFileLoader();
            var consoleWriter = new TextWriter(Console.Out);
            var importer = new MongoDbDataImporter(textFileLoader);
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
