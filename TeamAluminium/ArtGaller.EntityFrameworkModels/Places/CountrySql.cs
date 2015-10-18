
namespace ArtGallery.EntityFrameworkModels.Places
{
    using ArtGallery.EntityFrameworkModels.People;
    using System.Collections.Generic;

    public class CountrySql
    {
        private ICollection<ArtistSql> artist;

        public CountrySql()
        {
            this.artist = new HashSet<ArtistSql>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<ArtistSql> Artist
        {
            get { return this.artist; }

            set { this.artist = value; }
        }
    }
}
