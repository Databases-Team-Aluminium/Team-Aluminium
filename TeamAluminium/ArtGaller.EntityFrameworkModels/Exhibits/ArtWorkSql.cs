namespace ArtGallery.EntityFrameworkModels.Exhibits
{

    using System;
    using ArtGallery.Models.Common;
    using ArtGallery.EntityFrameworkModels.People;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;

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
