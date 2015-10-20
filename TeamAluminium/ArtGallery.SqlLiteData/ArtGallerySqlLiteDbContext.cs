namespace ArtGallery.SqlLiteData
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
   
    using SqlLiteModels;

    public class ArtGallerySqlLiteDbContext : DbContext
    {
        public ArtGallerySqlLiteDbContext()
            : base("ArtGallertSqlLite")
        {
            Configuration.ProxyCreationEnabled = true;
            Configuration.LazyLoadingEnabled = true;
        }

        public virtual IDbSet<ArtWorkDetails> ArtWorksDetails { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            var initializer = new ArtGalleryDbInitializer(modelBuilder);
            Database.SetInitializer(initializer);
        }
    }
}
