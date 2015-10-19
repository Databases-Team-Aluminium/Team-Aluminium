namespace GenerateExelReportsFromMySqlAndSqlLite
{
    using ArtGallery.MySqlData;
    using ArtGallery.MySqlModel.Places;

    public class MySqlAndSqlLiteToExcelReport
    {
        private static MySqlAndSqlLiteToExcelReport instance;

        public static MySqlAndSqlLiteToExcelReport Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MySqlAndSqlLiteToExcelReport();
                }

                return instance;
            }
        }

        private MySqlAndSqlLiteToExcelReport()
        {
            
        }

        public void Run()
        {
             //string PathToReportsArchive = @"../../Data/SalesReports.zip";
             //string PathToReports = @"../../Data/SalesReports";

            var data = new ArtGalleryMySqlDbContext();

            data.Country.Add (new CountryMySql
            {
                Name = "Canada" 
            });

            data.SaveChanges();
        }
    }
}
