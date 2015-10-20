namespace ArtGallery.SqlServerModels.Exhibits
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using ArtGallery.SqlServerModels.People;
    using ArtGallery.MongoDbModels.Common;
    using Additional;

    public class ArtWork
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Title { get; set; }

        public ArtWorkType Type { get; set; }

        public ArtWorkStatus Status { get; set; }

        public int ArtistId { get; set; }

        public virtual Artist Artist { get; set; }

        public decimal Value { get; set; }

        [Column(TypeName = "Date")]
        public DateTime? DateSold { get; set; }

        public virtual ArtWorkDescription Description { get; set; }
    }
}
