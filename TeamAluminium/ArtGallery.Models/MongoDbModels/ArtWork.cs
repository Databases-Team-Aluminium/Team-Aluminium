namespace ArtGallery.Models.MongoDbModels
{
    using Common;

    public class ArtWork
    {
        public int Id { get; set; }
        
        public string Title { get; set; }
        
        public ArtWorkType Type { get; set; }
        
        public ArtWorkStatus Status { get; set; }

        public Artist Artist { get; set; }
    }
}
