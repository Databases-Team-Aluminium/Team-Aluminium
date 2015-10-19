namespace ArtGallery.MySqlData
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class ArtGalleryMySqlDbInitializer : CreateDatabaseIfNotExists<ArtGalleryMySqlDbContext>
    {
        protected override void Seed(ArtGalleryMySqlDbContext data)
        {
        }
    }
}
