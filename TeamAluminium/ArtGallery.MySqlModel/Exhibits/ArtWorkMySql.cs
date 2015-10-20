namespace ArtGallery.MySqlModel.Exhibits
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using ArtGallery.MongoDbModels.Common;
    using ArtGallery.MySqlModel.People;

    public class ArtWorkMySql
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Title { get; set; }

        public ArtWorkType Type { get; set; }

        public ArtWorkStatus Status { get; set; }

        public int ArtistId { get; set; }

        public virtual ArtistMySql Artist { get; set; }

        public decimal Value { get; set; }

        public DateTime? DateSold { get; set; }
    }
}
