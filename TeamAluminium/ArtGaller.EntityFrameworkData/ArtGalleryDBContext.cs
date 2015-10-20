namespace ArtGallery.SqlServerData
{
    using System.Data.Entity;

    using Migrations;
    using SqlServerModels.Exhibits;
    using SqlServerModels.People;
    using SqlServerModels.Places;
    using SqlServerModels.SalesReport;
    using SqlServerModels.Additional;

    public class ArtGalleryDbContext : DbContext
    {
        public ArtGalleryDbContext()
            : base("ArtGallery")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ArtGalleryDbContext, Configuration>());
            // Database.SetInitializer(new DropCreateDatabaseAlways<ArtGalleryDbContext>());
        }

        public virtual IDbSet<Artist> Artists { get; set; }

        public virtual IDbSet<ArtWork> ArtWorks { get; set; }

        public virtual IDbSet<Country> Countries { get; set; }

        public virtual IDbSet<YearSaleReport> YearSalesReports { get; set; }

        public virtual IDbSet<SalesReport> SalesReports { get; set; }

        public virtual IDbSet<ArtWorkDescription> ArtWorksDescriptions { get; set; }
    }
}
