namespace ArtGallery.SqlServerModels.Places
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using People;

    public class Country
    {
        private ICollection<Artist> artist;

        public Country()
        {
            this.artist = new HashSet<Artist>();
        }

        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        public virtual ICollection<Artist> Artist
        {
            get { return this.artist; }

            set { this.artist = value; }
        }
    }
}
