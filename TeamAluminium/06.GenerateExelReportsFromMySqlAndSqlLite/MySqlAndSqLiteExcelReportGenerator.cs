namespace GenerateExcelReportFromMySqlAndSqLite
{
    using System;
    using System.Collections.Generic;

    using ArtGallery.MySqlData;
    using ArtGallery.SqlLiteData;
    using ArtGallery.SqlLiteModels;
    using DataLoaders;
    using DataLoaders.Contracts;
    using NPOI.XSSF.UserModel;
    using System.Linq;
    using ArtGallery.MySqlModel.Reports;
    using System.IO;

    public class MySqlAndSqLiteExcelReportGenerator
    {
        private const string PathToExcelReport = "../../../Output/Combined-Db-Report.xlsx";
        private static MySqlAndSqLiteExcelReportGenerator instance;
        private readonly IDataLoader dataLoader;

        private MySqlAndSqLiteExcelReportGenerator(IDataLoader loader)
        {
            this.dataLoader = loader;
        }

        public static MySqlAndSqLiteExcelReportGenerator Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MySqlAndSqLiteExcelReportGenerator(new TextFileLoader());
                }

                return instance;
            }
        }

        public void Run()
        {
            Console.WriteLine("Generating SQLite data...");
            this.GenerateSqLiteRecords();
            Console.WriteLine("Done.");

            Console.WriteLine("Generating combined Excel 2007 report...");
            this.GenerateExcelReport();
            Console.WriteLine("Done.");
        }

        private void GenerateSqLiteRecords()
        {
            using (var db = new ArtGallerySqlLiteDbContext())
            {
                var random = new Random();
                ICollection<string> art = this.dataLoader.GetArtworks();
                foreach (var item in art)
                {
                    var details = new ArtWorkDetails
                    {
                        ArtWorkName = item,
                        NumberOfLayersOfMaterial = random.Next(1, 6),
                        Weight = ((decimal)random.Next(10, 1000)) / 10
                    };

                    db.ArtWorksDetails.Add(details);
                }

                db.SaveChanges();
            }
        }

        private void GenerateExcelReport()
        {
            List<ArtWorkDetails> details;
            List<ArtWorkJsonReport> reports;
            using (var mySql = new ArtGalleryMySqlDbContext())
            {
                reports = mySql.Reports.ToList();
                using (var sqLite = new ArtGallerySqlLiteDbContext())
                {
                    details = sqLite.ArtWorksDetails.ToList();
                }
            }

            var joinedData = from report in reports
                             join detail in details
                             on report.Name equals detail.ArtWorkName
                             select new
                             {
                                 Name = report.Name,
                                 Type = report.Type,
                                 Income = report.Income,
                                 Weight = detail.Weight,
                                 Layers = detail.NumberOfLayersOfMaterial
                             };

            // Name, Type, Income, Weight, Materials Layers

            int rowNumber = 0;

            var doc = new XSSFWorkbook();
            var sheet = (XSSFSheet)doc.CreateSheet();
            var firstRow = sheet.CreateRow(rowNumber++);
            var nameTitle = firstRow.CreateCell(0, NPOI.SS.UserModel.CellType.String);
            nameTitle.SetCellValue("Name");
            var typeTitle = firstRow.CreateCell(1, NPOI.SS.UserModel.CellType.String);
            typeTitle.SetCellValue("Type");
            var incomeTitle = firstRow.CreateCell(2, NPOI.SS.UserModel.CellType.String);
            incomeTitle.SetCellValue("Income");
            var weightTitle = firstRow.CreateCell(3, NPOI.SS.UserModel.CellType.String);
            weightTitle.SetCellValue("Weight");
            var layersTitle = firstRow.CreateCell(4, NPOI.SS.UserModel.CellType.String);
            layersTitle.SetCellValue("Materials Layers");

            foreach (var item in joinedData)
            {
                var row = sheet.CreateRow(rowNumber++);
                var nameCell = row.CreateCell(0, NPOI.SS.UserModel.CellType.String);
                nameCell.SetCellValue(item.Name);
                var typeCell = row.CreateCell(1, NPOI.SS.UserModel.CellType.String);
                typeCell.SetCellValue(item.Type);
                var incomeCell = row.CreateCell(2, NPOI.SS.UserModel.CellType.Numeric);
                incomeCell.SetCellValue((double)item.Income);
                var weightCell = row.CreateCell(3, NPOI.SS.UserModel.CellType.Numeric);
                weightCell.SetCellValue((double)item.Weight);
                var layersCell = row.CreateCell(4, NPOI.SS.UserModel.CellType.Numeric);
                layersCell.SetCellValue(item.Layers);
            }

            sheet.DefaultColumnWidth = 30;

            using (var fs = new FileStream(PathToExcelReport, FileMode.Create, FileAccess.Write))
            {
                doc.Write(fs);
            }
        }
    }
}
