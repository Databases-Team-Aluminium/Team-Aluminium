namespace ArtGallery.Setup.DataLoaders.Contracts
{
    using System.Collections.Generic;

    using MongoDbModels.Exhibits;
    using MongoDbModels.People;
    using MongoDbModels.Places;
    using MongoDbModels.Structures;

    public interface IDataLoader
    {
        ICollection<Artist> GetArtists();

        ICollection<ArtWork> GetArtworks();

        ICollection<Country> GetCountries();
       
        ICollection<Department> GetDepartments();

        ICollection<Employee> GetEmployees();
    }
}
