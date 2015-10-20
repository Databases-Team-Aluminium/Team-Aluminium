namespace ArtGallery.SqlServerData
{
    using ArtGallery.SqlServerData.Repositories;
    using ArtGallery.SqlServerModels.Exhibits;
    using ArtGallery.SqlServerModels.People;
    using ArtGallery.SqlServerModels.Places;
    using ArtGallery.SqlServerModels.SalesReport;

    public interface IArtGalleryData
    {
        IRepository<Artist> Artists { get; }

        IRepository<ArtWork> ArtWorks { get; }

        IRepository<Country> Country { get; }

        IRepository<YearSaleReport> YearSaleReport { get; }

        IRepository<SalesReport> SaleReport { get; }

        int SaveChanges();
    }
}
