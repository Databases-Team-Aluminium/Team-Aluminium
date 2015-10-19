namespace ArtGallery.MySqlModel.Places
{
    using ArtGallery.MySqlModel.People;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class CountryMySql
    {
        private ICollection<ArtistMySql> artist;

        public CountryMySql()
        {
            this.artist = new HashSet<ArtistMySql>();
        }

        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        public virtual ICollection<ArtistMySql> Artist
        {
            get { return this.artist; }

            set { this.artist = value; }
        }
    }
}
