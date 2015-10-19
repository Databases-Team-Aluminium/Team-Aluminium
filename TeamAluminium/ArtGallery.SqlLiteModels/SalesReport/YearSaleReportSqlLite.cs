namespace ArtGallery.SqlLiteModels.SalesReport
{
    using System;
    using System.Collections.Generic;

    public class YearSaleReportSqlLite
    {
        private ICollection<SaleReportsqlLite> saleReports;

        public YearSaleReportSqlLite()
        {
            this.saleReports = new HashSet<SaleReportsqlLite>();
        }

        public int Id { get; set; }

        public int Year { get; set; }

        public DateTime ReportDate { get; set; }

        public virtual ICollection<SaleReportsqlLite> SaleReports
        {
            get { return this.saleReports; }

            set { this.saleReports = value; }
        }
    }
}
