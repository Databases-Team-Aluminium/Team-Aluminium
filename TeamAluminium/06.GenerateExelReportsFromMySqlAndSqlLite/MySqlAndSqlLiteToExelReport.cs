namespace GenerateExelReportsFromMySqlAndSqlLite
{
    using System;

    using ArtGallery.MySqlData;
    using ArtGallery.MySqlModel.Places;
    using ArtGallery.SqlLiteData;
    using ArtGallery.SqlLiteModels.SalesReport;

    public class MySqlAndSqlLiteToExelReport
    {
        private static MySqlAndSqlLiteToExelReport instance;

        private MySqlAndSqlLiteToExelReport()
        {
        }

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

        public void Run()
        {
            var dataMySql = new ArtGalleryMySqlDbContext();
            var dataSqlLite = new ArtGallerySqlLiteDbContext();

            dataMySql.Country.Add(new CountryMySql
            {
                Name = "Canada"
            });

            dataMySql.SaveChanges();

            dataSqlLite.YearSaleReport.Add(new YearSaleReportSqlLite
            {
                ReportDate = DateTime.Now,
                Year = DateTime.Now.Year
            });

            dataSqlLite.SaveChanges();
        }
    }
}
