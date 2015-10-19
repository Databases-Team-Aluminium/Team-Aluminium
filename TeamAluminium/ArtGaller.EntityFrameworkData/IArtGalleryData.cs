namespace ArtGallery.EntityFrameworkData
{
    using ArtGallery.EntityFrameworkData.Repositories;
    using ArtGallery.EntityFrameworkModels.Exhibits;
    using ArtGallery.EntityFrameworkModels.People;
    using ArtGallery.EntityFrameworkModels.Places;
    using ArtGallery.EntityFrameworkModels.SalesReport;

    public interface IArtGalleryData
    {
        IRepository<ArtistSql> Artists { get; }

        IRepository<ArtWorkSql> ArtWorks { get; }

        IRepository<CountrySql> Country { get; }

        IRepository<YearSaleReport> YearSaleReport { get; }

        IRepository<SalesReport> SaleReport { get; }

        int SaveChanges();
    }
}
