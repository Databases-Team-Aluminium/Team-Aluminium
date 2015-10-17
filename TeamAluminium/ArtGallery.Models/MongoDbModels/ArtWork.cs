namespace ArtGallery.Models.MongoDbModels
{
    using System;

    using Common;

    public class ArtWork
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public ArtWorkType Type { get; set; }

        public ArtWorkStatus Status { get; set; }

        public int ArtistId { get; set; }

        public decimal Value { get; set; }

        public DateTime DateSold { get; set; }
    }
}
