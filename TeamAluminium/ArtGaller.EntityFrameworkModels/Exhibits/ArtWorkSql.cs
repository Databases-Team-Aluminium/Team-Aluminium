namespace ArtGallery.EntityFrameworkModels.Exhibits
{

    using System;
    using ArtGallery.Models.Common;
    using ArtGallery.EntityFrameworkModels.People;

    public class ArtWorkSql
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public ArtWorkType Type { get; set; }

        public ArtWorkStatus Status { get; set; }

        public int ArtistId { get; set; }

        public virtual ArtistSql Artist { get; set; }

        public decimal Value { get; set; }

        public DateTime DateSold { get; set; }
    }
}
