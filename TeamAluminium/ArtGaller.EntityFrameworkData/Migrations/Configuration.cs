namespace ArtGallery.SqlServerData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<ArtGallery.SqlServerData.ArtGalleryDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
            this.ContextKey = "ArtGaller.EntityFrameworkData.ArtGalleryDbContext";
        }

        protected override void Seed(ArtGallery.SqlServerData.ArtGalleryDbContext context)
        {
        }
    }
}
