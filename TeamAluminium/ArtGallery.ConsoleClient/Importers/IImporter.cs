namespace ArtGallery.ConsoleClient.Importers
{
    using ArtGallery.Models.MongoDbModels;
    using System.Collections.Generic;

  public  interface IImporter
    {
      ICollection<Artist> GetArtists();

      ICollection<ArtWork> GetArtworks();
    }
}
