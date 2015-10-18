namespace LoadExcelReportsImportDataMongoDbToMsSql
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    using ArtGaller.EntityFrameworkData;
    using ArtGaller.EntityFrameworkData.Migrations;
    using ArtGallery.ConsoleClient;
    using ArtGallery.ConsoleClient.Writers;

    public class Startup
    {
        public static void Main()
        {
            /// string PathToReportsArchive = @"../../Data/SalesReports.zip";
           /// string PathToReports = @"../../Data/SalesReports";

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ArtGalleryDbContext, Configuration>());

            var data = new ArtGalleryDbContext();
            var dataImporter = new MongoDb();
            var consoleWriter = new TextWriter(Console.Out);

            var sqlDbDataImporter = new MsSqlDbDataImporter(dataImporter, data);
            var archiver = new ArchiveHandler();

            sqlDbDataImporter.Subscribe(consoleWriter);

            /// msSqlDbDataImporter.ImportData();
            archiver.Subscribe(consoleWriter);

            /// archiver.UnzipToFolder(PathToReportsArchive, PathToReports);

            var transfer = new TransferDataFromExcelToDB(data);

            transfer.GetFile();
        }
    }
}
