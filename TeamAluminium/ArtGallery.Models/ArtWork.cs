namespace ArtGallery.Models
{
    public abstract class ArtWork
    {
        public int Id { get; set; }
        
        public string Title { get; set; }
        
        public ArtWorkType Type { get; set; }
        
        public ArtWorkStatus Status { get; set; }
        
        public int ArtistId { get; set; }

        public virtual Artist Artist { get; set; }
    }
}
