namespace ArtGallery.Setup.DataLoaders.Contracts
{
    using System.Collections.Generic;

    using Models.Exhibits;
    using Models.People;
    using Models.Places;
    using Models.Structures;

    public interface IDataLoader
    {
        ICollection<Artist> GetArtists();

        ICollection<ArtWork> GetArtworks();

        ICollection<Country> GetCountries();
       
        ICollection<Department> GetDepartments();

        ICollection<Employee> GetEmployees();
    }
}
