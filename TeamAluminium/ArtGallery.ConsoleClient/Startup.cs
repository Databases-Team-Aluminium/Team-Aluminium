﻿namespace ArtGallery.ConsoleClient
{
    using CreateJsonReports;
    using GenerateExelReportsFromMySqlAndSqlLite;
    using GeneratePdfReports;
    using LoadExcelReportsImportDataMongoDbToMsSql;
    using LoadXmlIntoMongoDbAndMsSql;
    using Setup;
    using System.IO;

    public class Startup
    {
        private const string OutputDirectoryPath = "../../../Output";
        private const string PathToPdfReports = "../../../Output/Pdf-Reports";
        private const string PathToJsonReports = "../../../Output/Json-Reports";

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
        ///     6. Task 3 -
        ///     7. Task 4 - Generate JSON reports
        ///     8. Task 5 - Load XML into MongoDb and SQL Server
        ///     9. Task 6 - 
        /// </summary>
        public static void Main()
        {
            // TODO: Task 1 - Excel to SQL Server importing / Toma
            // TODO: Task 3 - All
            // TODO: Task 5 - XML to SQL Server importing / Vasil
            // TODO: Task 6 - All

            CreateOutputDirectory();

            //SetupClient.Instance.Run();
            //MsSqlExcelAndMongoDbDataImporter.Instance.Run();
            //PdfReportsGenerator.Instance.Run();
            //JsonReportsGenerator.Instance.Run();
            //MongoDbAndMsSqlXmlDataImporter.Instance.Run();
            MySqlAndSqlLiteToExelReport.Instance.Run();
        }

        private static void CreateOutputDirectory()
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
        }
    }
}
