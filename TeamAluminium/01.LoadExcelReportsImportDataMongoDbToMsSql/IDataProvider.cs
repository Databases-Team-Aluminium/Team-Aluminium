namespace LoadExcelReportsImportDataMongoDbToMsSql
{
    using System;
    using System.Collections.Generic;

    using ArtGallery.Models.Exhibits;
    using ArtGallery.Models.People;
    using ArtGallery.Models.Places;

    public interface IDataProvider
    {
        ICollection<Artist> GetArtists();

        ICollection<ArtWork> GetArtWorks();

        ICollection<Country> GetCountries();
    }
}
