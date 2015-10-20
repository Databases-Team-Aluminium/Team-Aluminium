namespace ArtGallery.MySqlData
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;

    using MySqlModel.Exhibits;
    using MySqlModel.People;
    using MySqlModel.Places;
    using MySqlModel.Reports;

    [DbConfigurationType(typeof(MySqlConfiguration))]
    public class ArtGalleryMySqlDbContext : DbContext
    {
        public ArtGalleryMySqlDbContext()
            : base("ArtGalleryMySql")
        {
            Database.SetInitializer<ArtGalleryMySqlDbContext>(new ArtGalleryMySqlDbInitializer());
        }

        public virtual IDbSet<ArtistMySql> Artists { get; set; }

        public virtual IDbSet<ArtWorkMySql> ArtWorks { get; set; }

        public virtual IDbSet<CountryMySql> Country { get; set; }

        public virtual IDbSet<ArtWorkJsonReport> Reports { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
