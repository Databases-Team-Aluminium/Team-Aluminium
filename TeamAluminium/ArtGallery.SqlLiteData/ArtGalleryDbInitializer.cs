namespace ArtGallery.SqlLiteData
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    using SQLite.CodeFirst;

    public class ArtGalleryDbInitializer : SqliteDropCreateDatabaseAlways<ArtGallerySqlLiteDbContext>
    {
        public ArtGalleryDbInitializer(DbModelBuilder modelBuilder)
            : base(modelBuilder)
        {
        }

        protected override void Seed(ArtGallerySqlLiteDbContext context)
        {
        }
    }
}
