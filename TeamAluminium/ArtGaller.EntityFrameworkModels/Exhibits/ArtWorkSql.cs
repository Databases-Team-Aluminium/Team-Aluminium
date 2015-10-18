namespace ArtGallery.EntityFrameworkModels.Exhibits
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using ArtGallery.EntityFrameworkModels.People;
    using ArtGallery.Models.Common;

    public class ArtWorkSql
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Title { get; set; }

        public ArtWorkType Type { get; set; }

        public ArtWorkStatus Status { get; set; }

        public int ArtistId { get; set; }

        public virtual ArtistSql Artist { get; set; }

        public decimal Value { get; set; }

        [Column(TypeName = "DateTime2")]
        public DateTime DateSold { get; set; }
    }
}
