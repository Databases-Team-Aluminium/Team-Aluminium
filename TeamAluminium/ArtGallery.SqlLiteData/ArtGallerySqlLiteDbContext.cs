namespace ArtGallery.SqlLiteData
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Linq;

    using ArtGallery.SqlLiteModels.SalesReport;

    public class ArtGallerySqlLiteDbContext : DbContext
    {
        public ArtGallerySqlLiteDbContext()
            : base("ArtGallertSqlLite")
        {
            Configuration.ProxyCreationEnabled = true;
            Configuration.LazyLoadingEnabled = true;
        }

        public virtual IDbSet<YearSaleReportSqlLite> YearSaleReport { get; set; }

        public virtual IDbSet<SaleReportsqlLite> SaleReport { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            var initializer = new ArtGalleryDbInitializer(modelBuilder);
            Database.SetInitializer(initializer);
        }
    }
}
