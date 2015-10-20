namespace ArtGallery.ConsoleClient
{
    using System.IO;
    using CreateJsonReports;
    using GenerateExcelReportFromMySqlAndSqLite;
    using GeneratePdfReports;
    using LoadExcelReportsImportDataMongoDbToMsSql;
    using LoadXmlIntoMongoDbAndMsSql;
    using Setup;
    using GenerateXmlReport;

    public class Startup
    {
        private const string OutputDirectoryPath = "../../../Output";
        private const string PathToPdfReports = "../../../Output/Pdf-Reports";
        private const string PathToJsonReports = "../../../Output/Json-Reports";
        private const string PathToXmlReports = "../../../Output/Xml-Reports";
        private const string PathToSqlitе = "../../../Output/SqliteDb";
        private const string PathToSqliteDb = "../../../Output/SqliteDb/ArtGallery.sqlite";

        /// <summary>
        /// Entry point for the console application executing 
        /// all tasks defined in the teamwork assignment.
        /// Execution flow:
        ///     1. Create a MongoDb database
        ///     2. Fill the database with meaningful data
        ///     3. Generate Excel reports using meaningful 
        ///        and randomly generated data from the db
        ///        and zip them
        ///     4. Task 1 - Transfer the data from MongoDb
        ///        to a SQL Server database and insert the
        ///        data from the Excel reports in the newly
        ///        created SQL Server db
        ///     5. Task 2 - Generate PDF reports using
        ///        aggregated data from the SQL Server db
        ///     6. Task 3 - Generate XML reports
        ///     7. Task 4 - Generate JSON reports
        ///     8. Task 5 - Load XML into MongoDb and SQL Server
        ///     9. Task 6 - Generate Excel 2007 report
        ///        combining MySQL reports and SQLite information
        /// </summary>
        public static void Main()
        {
            // TODO: REFACTOR

            // Preparation
            CreateOutputDirectoryAndSubDirectories();

            // Initialization
            SetupClient.Instance.Run();

            // Tasks
            MsSqlExcelAndMongoDbDataImporter.Instance.Run();
            PdfReportsGenerator.Instance.Run();
            XmlReportsGenerator.Instance.Run();
            JsonReportsGenerator.Instance.Run();
            MongoDbAndMsSqlXmlDataImporter.Instance.Run();
            MySqlAndSqLiteExcelReportGenerator.Instance.Run();
        }

        private static void CreateOutputDirectoryAndSubDirectories()
        {
            if (!Directory.Exists(OutputDirectoryPath))
            {
                Directory.CreateDirectory(OutputDirectoryPath);
            }

            if (!Directory.Exists(PathToPdfReports))
            {
                Directory.CreateDirectory(PathToPdfReports);
            }

            if (!Directory.Exists(PathToJsonReports))
            {
                Directory.CreateDirectory(PathToJsonReports);
            }

            if (!Directory.Exists(PathToXmlReports))
            {
                Directory.CreateDirectory(PathToXmlReports);
            }

            if (!Directory.Exists(PathToSqlitе))
            {
                Directory.CreateDirectory(PathToSqlitе);
            }

            if (!File.Exists(PathToSqliteDb))
            {
                File.Create(PathToSqliteDb);
            }
        }
    }
}
