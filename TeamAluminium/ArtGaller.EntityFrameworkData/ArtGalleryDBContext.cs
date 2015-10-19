namespace ArtGallery.EntityFrameworkData
{
    using System.Data.Entity;

    using Migrations;
    using EntityFrameworkModels.Exhibits;
    using EntityFrameworkModels.People;
    using EntityFrameworkModels.Places;
    using EntityFrameworkModels.SalesReport;
    using EntityFrameworkModels.Additional;

    public class ArtGalleryDbContext : DbContext
    {
        public ArtGalleryDbContext()
            : base("ArtGallery")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ArtGalleryDbContext, Configuration>());
            // Database.SetInitializer(new DropCreateDatabaseAlways<ArtGalleryDbContext>());
        }

        public virtual IDbSet<ArtistSql> Artists { get; set; }

        public virtual IDbSet<ArtWorkSql> ArtWorks { get; set; }

        public virtual IDbSet<CountrySql> Country { get; set; }

        public virtual IDbSet<YearSaleReport> YearSaleReport { get; set; }

        public virtual IDbSet<SalesReport> SalesReport { get; set; }

        public virtual IDbSet<ArtWorkDescription> ArtWorksDescriptions { get; set; }
    }
}
