namespace ArtGallery.ConsoleClient.Importers
{
    using ArtGallery.Models.Exhibits;
    using ArtGallery.Models.People;
    using ArtGallery.Models.Places;
    using ArtGallery.Models.Structures;
    using System.Collections.Generic;

    public interface IImporter
    {
        ICollection<Artist> GetArtists();

        ICollection<ArtWork> GetArtworks();

        ICollection<Country> GetCountries();
       
        ICollection<Department> GetDepartments();

        ICollection<Employee> GetEmployees();
    }
}
