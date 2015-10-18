namespace ArtGallery.EntityFrameworkModels.Places
{
    using ArtGallery.EntityFrameworkModels.People;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class CountrySql
    {
        private ICollection<ArtistSql> artist;

        public CountrySql()
        {
            this.artist = new HashSet<ArtistSql>();
        }

        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        public virtual ICollection<ArtistSql> Artist
        {
            get { return this.artist; }

            set { this.artist = value; }
        }
    }
}
