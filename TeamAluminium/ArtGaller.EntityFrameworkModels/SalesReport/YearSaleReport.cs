namespace ArtGallery.EntityFrameworkModels.SalesReport
{
    using System;
    using System.Collections.Generic;

    public class YearSaleReport
    {
        private ICollection<SalesReport> saleReports;

        public YearSaleReport()
        {
            this.saleReports = new HashSet<SalesReport>();
        }

        public int Id { get; set; }

        public int Year { get; set; }

        public DateTime ReportDate { get; set; }

        public virtual ICollection<SalesReport> SaleReports
        {
            get { return this.saleReports; }

            set { this.saleReports = value; }
        }
    }
}
