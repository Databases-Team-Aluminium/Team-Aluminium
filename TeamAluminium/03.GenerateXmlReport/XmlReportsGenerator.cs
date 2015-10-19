namespace GenerateXmlReport
{
    using System.Globalization;
    using System.Xml;
    using ArtGallery.EntityFrameworkData;
    using System.Data.Entity.Infrastructure;

    public class XmlReportsGenerator
    {
        private const string XmlReportFilePath = "../../../Output/Xml-Reports/Sales-by-Artists-report.xml";

        private static XmlReportsGenerator instance;
        
        public static XmlReportsGenerator Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new XmlReportsGenerator();
                }

                return instance;
            }
        }

        private XmlReportsGenerator()
        {
        }

        public void Run()
        {
            var db = new ArtGalleryDbContext();
            GenerateXmlReport(db, XmlReportFilePath);
        }

        private static void GenerateXmlReport(ArtGalleryDbContext db, string xmlReportFilePath)
        {
            var xmlDocument = new XmlDocument();
            XmlElement rootElement = xmlDocument.CreateElement("sales");


            string nativeSqlQuery =
                    "SELECT a.FirstName + ' ' + a.LastName AS [FullName], aw.DateSold, SUM(aw.Value) AS [Price]" +
                    "FROM ArtWorkSqls aw " +
                    "INNER JOIN ArtistSqls a on aw.ArtistId=a.Id AND aw.DateSold IS NOT NULL " +
                    "GROUP BY a.FirstName, a.LastName, aw.DateSold";

            DbRawSqlQuery<ArtistSaleReport> artistSaleReports = db.Database.SqlQuery<ArtistSaleReport>(nativeSqlQuery);

            foreach (var artistSaleReport in artistSaleReports)
            {
                XmlElement saleElement = xmlDocument.CreateElement("sale");
                saleElement.SetAttribute("artist", artistSaleReport.FullName);

                XmlElement summaryElement = xmlDocument.CreateElement("summary");
                summaryElement.SetAttribute("date", artistSaleReport.DateSold.ToString("dd-MMM-yyyy", CultureInfo.GetCultureInfo("en-US")));
                summaryElement.SetAttribute("total-sum", artistSaleReport.Price.ToString());
                saleElement.AppendChild(summaryElement);
                rootElement.AppendChild(saleElement);
            }

            xmlDocument.AppendChild(rootElement);

            XmlWriter xmlReportFile = GenerateXmlFile(xmlReportFilePath);
            xmlReportFile.WriteStartDocument();
            using (xmlReportFile)
            {
                xmlDocument.WriteTo(xmlReportFile);
            }
        }

        private static XmlWriter GenerateXmlFile(string fileName)
        {
            XmlWriter file = XmlWriter.Create(fileName);

            return file;
        }
    }
}

