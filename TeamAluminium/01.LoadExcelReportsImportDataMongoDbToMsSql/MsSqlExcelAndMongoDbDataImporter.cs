namespace LoadExcelReportsImportDataMongoDbToMsSql
{
    using System;

    using ArtGallery.EntityFrameworkData;
    using ArtGallery.Setup;
    using ArtGallery.Setup.Writers;

    public class MsSqlExcelAndMongoDbDataImporter
    {
        private static MsSqlExcelAndMongoDbDataImporter instance;

        public static MsSqlExcelAndMongoDbDataImporter Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MsSqlExcelAndMongoDbDataImporter();
                }

                return instance;
            }
        }

        private MsSqlExcelAndMongoDbDataImporter()
        {
            
        }

        public void Run()
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
