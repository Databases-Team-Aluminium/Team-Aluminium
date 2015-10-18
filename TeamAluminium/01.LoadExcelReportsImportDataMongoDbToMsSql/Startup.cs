namespace LoadExcelReportsImportDataMongoDbToMsSql
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    using ArtGallery.EntityFrameworkData;
    using ArtGallery.EntityFrameworkData.Migrations;
    using ArtGallery.ConsoleClient;
    using ArtGallery.ConsoleClient.Writers;

    public class Startup
    {
        public static void Main()
        {
            /// string PathToReportsArchive = @"../../Data/SalesReports.zip";
            /// string PathToReports = @"../../Data/SalesReports";

            var data = new ArtGalleryDbContext();
            var dataImporter = new MongoDb();
            var consoleWriter = new TextWriter(Console.Out);

            var sqlDbDataImporter = new MsSqlDbDataImporter(dataImporter, data);
            var archiver = new ArchiveHandler();

            sqlDbDataImporter.Subscribe(consoleWriter);

            sqlDbDataImporter.ImportData();
            archiver.Subscribe(consoleWriter);

            /// archiver.UnzipToFolder(PathToReportsArchive, PathToReports);
            var transfer = new TransferDataFromExcelToDB(data);

            transfer.GetFile();
        }
    }
}
