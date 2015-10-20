namespace LoadExcelReportsImportDataMongoDbToMsSql
{
    using System;
    using System.Collections.Generic;

    using ArtGallery.MongoDbModels.Exhibits;
    using ArtGallery.MongoDbModels.People;
    using ArtGallery.MongoDbModels.Places;

    public interface IDataProvider
    {
        ICollection<Artist> GetArtists();

        ICollection<ArtWork> GetArtWorks();

        ICollection<Country> GetCountries();
    }
}
