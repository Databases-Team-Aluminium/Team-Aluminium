namespace ArtGaller.EntityFrameworkData
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    using ArtGallery.EntityFrameworkModels.Exhibits;
    using ArtGallery.EntityFrameworkModels.People;
    using ArtGallery.EntityFrameworkModels.Places;
    using ArtGallery.EntityFrameworkModels.SalesReport;

    public class ArtGalleryDbContext : DbContext
    {
        public ArtGalleryDbContext()
            : base("ArtGallery")
        {
        }

        public virtual IDbSet<ArtistSql> Artists { get; set; }

        public virtual IDbSet<ArtWorkSql> ArtWorks { get; set; }

        public virtual IDbSet<CountrySql> Country { get; set; }

        public virtual IDbSet<YearSaleReport> YearSaleReport { get; set; }

        public virtual IDbSet<SaleReport> SaleReport { get; set; }
    }
}
