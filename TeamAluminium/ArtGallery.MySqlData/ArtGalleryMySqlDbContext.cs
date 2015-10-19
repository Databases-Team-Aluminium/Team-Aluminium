namespace ArtGallery.MySqlData
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;

    using ArtGallery.MySqlModel.Exhibits;
    using ArtGallery.MySqlModel.People;
    using ArtGallery.MySqlModel.Places;
   
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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
