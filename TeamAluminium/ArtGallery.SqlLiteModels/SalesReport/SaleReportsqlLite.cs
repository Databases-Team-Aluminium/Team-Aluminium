namespace ArtGallery.SqlLiteModels.SalesReport
{
    public class SaleReportsqlLite
    {
        public int Id { get; set; }

        public string ItemName { get; set; }

        public decimal Price { get; set; }

        public int YearSaleReportId { get; set; }

        public virtual YearSaleReportSqlLite YearSaleReport { get; set; }
    }
}
