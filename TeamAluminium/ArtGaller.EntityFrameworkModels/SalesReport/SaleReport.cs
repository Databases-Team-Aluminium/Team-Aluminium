namespace ArtGallery.SqlServerModels.SalesReport
{
    public class SalesReport
    {
        public int Id { get; set; }

        public string ItemName { get; set; }

        public decimal Price { get; set; }

        public int YearSaleReportId { get; set; }

        public virtual YearSaleReport YearSaleReport { get; set; }
    }
}
