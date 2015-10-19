namespace GenerateExelReportsFromMySqlAndSqlLite
{
    using ArtGallery.MySqlData;
    using ArtGallery.MySqlModel.Places;
    using System;

    public class MySqlAndSqlLiteToExelReport
    {
        private static MySqlAndSqlLiteToExelReport instance;

        public static MySqlAndSqlLiteToExelReport Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MySqlAndSqlLiteToExelReport();
                }

                return instance;
            }
        }

        private MySqlAndSqlLiteToExelReport()
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
