namespace ArtGallery.SqlServerData
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;

    using ArtGallery.SqlServerData.Repositories;
    using ArtGallery.SqlServerModels.Exhibits;
    using ArtGallery.SqlServerModels.People;
    using ArtGallery.SqlServerModels.Places;
    using ArtGallery.SqlServerModels.SalesReport;

    public class ArtGalleryData : IArtGalleryData
    {
        private DbContext context;
        private IDictionary<Type, object> repositories;

        public ArtGalleryData(DbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public IRepository<Artist> Artists
        {
            get { return this.GetRepository<Artist>(); }
        }

        public IRepository<ArtWork> ArtWorks
        {
            get { return this.GetRepository<ArtWork>(); }
        }

        public IRepository<Country> Country
        {
            get { return this.GetRepository<Country>(); }
        }

        public IRepository<YearSaleReport> YearSaleReport
        {
            get { return this.GetRepository<YearSaleReport>(); }
        }

        public IRepository<SalesReport> SaleReport
        {
            get { return this.GetRepository<SalesReport>(); }
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            var typeOfRepository = typeof(T);
            if (!this.repositories.ContainsKey(typeOfRepository))
            {
                var newRepository = Activator.CreateInstance(typeof(EFRepository<T>), this.context);
                this.repositories[typeOfRepository] = newRepository;
            }

            return (IRepository<T>)this.repositories[typeOfRepository];
        }
    }
}
