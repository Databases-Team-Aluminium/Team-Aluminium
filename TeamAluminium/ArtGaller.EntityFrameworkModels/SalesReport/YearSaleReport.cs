namespace ArtGallery.EntityFrameworkModels.SalesReport
{
    using System;
    using System.Collections.Generic;

    public class YearSaleReport
    {
        private ICollection<SaleReport> saleReports;

        public YearSaleReport()
        {
            this.saleReports = new HashSet<SaleReport>();
        }

        public int Id { get; set; }

        public int Year { get; set; }

        public DateTime ReportDate { get; set; }

        public virtual ICollection<SaleReport> SaleReports
        {
            get { return this.saleReports; }

            set { this.saleReports = value; }
        }
    }
}
