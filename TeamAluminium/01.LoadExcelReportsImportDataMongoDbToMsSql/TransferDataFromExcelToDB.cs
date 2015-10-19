namespace LoadExcelReportsImportDataMongoDbToMsSql
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.OleDb;
    using System.IO;

    using ArtGallery.EntityFrameworkData;
    using ArtGallery.EntityFrameworkModels.SalesReport;

    public class TransferDataFromExcelToDB
    {
        private const string PathToExcelReports = @"../../../Output/SalesReports";
        private const string ConnectionStringFormat = @"Provider=Microsoft.Jet.OLEDB.4.0;" +
             @"Data Source=../../../Output/SalesReports/{0};Extended Properties=Excel 8.0";
        private readonly string[] monthNames = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };

        private ArtGalleryDbContext data;

        public TransferDataFromExcelToDB(ArtGalleryDbContext data)
        {
            this.data = data;
        }

        public void GetFile()
        {
            var dir = new DirectoryInfo(PathToExcelReports);

            this.Traverse(dir);
        }

        public void Traverse(DirectoryInfo directory)
        {
            foreach (var dir in directory.GetDirectories())
            {
                var timeOfReport = this.ParceDate(dir.Name);
                var yearSaleReport = new YearSaleReport()
                {
                    Year = timeOfReport.Year,
                    ReportDate = timeOfReport
                };

                var dirPath = dir.Name + '\\';
                if (dir.GetFiles().Length != 0)
                {
                    foreach (var file in dir.GetFiles())
                    {
                        var filePath = dirPath + file.Name;
                        yearSaleReport.SaleReports = this.GetSaleReports(filePath);

                        this.data.YearSaleReport.Add(yearSaleReport);
                        this.data.SaveChanges();
                    }

                }

                this.Traverse(dir);
            }
        }

        private ICollection<SalesReport> GetSaleReports(string filePath)
        {
            var saleRepots = new List<SalesReport>();
            var oleDb = new OleDbConnection(string.Format(ConnectionStringFormat, filePath));
            oleDb.Open();
            using (oleDb)
            {
                DataTable sheets = oleDb.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                foreach (DataRow dataRow in sheets.Rows)
                {
                    string sheet = dataRow[2].ToString().Replace("'", string.Empty);
                    var oleDbCommand = new OleDbCommand("SELECT * FROM [" + sheet + "]", oleDb);

                    OleDbDataReader reader = oleDbCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        saleRepots.Add(new SalesReport
                        {
                            ItemName = (string)reader["Title"],
                            Price = (decimal)reader["SoldFor"]
                        });
                    }
                }
            }

            return saleRepots;
        }

        private DateTime ParceDate(string date)
        {
            var splitetdDate = date.Split(new[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
            var day = int.Parse(splitetdDate[0]);
            var month = Array.IndexOf(this.monthNames, splitetdDate[1]) + 1;
            var year = int.Parse(splitetdDate[2]);

            return new DateTime(year, month, day);
        }
    }
}
