namespace ArtGallery.EntityFrameworkData
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;

    using ArtGallery.EntityFrameworkData.Repositories;
    using ArtGallery.EntityFrameworkModels.Exhibits;
    using ArtGallery.EntityFrameworkModels.People;
    using ArtGallery.EntityFrameworkModels.Places;
    using ArtGallery.EntityFrameworkModels.SalesReport;

    public class ArtGalleryData : IArtGalleryData
    {
        private DbContext context;
        private IDictionary<Type, object> repositories;

        public ArtGalleryData(DbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public IRepository<ArtistSql> Artists
        {
            get { return this.GetRepository<ArtistSql>(); }
        }

        public IRepository<ArtWorkSql> ArtWorks
        {
            get { return this.GetRepository<ArtWorkSql>(); }
        }

        public IRepository<CountrySql> Country
        {
            get { return this.GetRepository<CountrySql>(); }
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
