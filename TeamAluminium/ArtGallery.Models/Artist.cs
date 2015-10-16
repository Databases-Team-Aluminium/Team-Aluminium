namespace ArtGallery.Models
{
    using System.Collections.Generic;

    public class Artist : Person
    {
        private ICollection<ArtWork> artWorks;

        public Artist() : base()
        {
            this.artWorks = new HashSet<ArtWork>();
        }

        public virtual ICollection<ArtWork> ArtWorks
        {
            get
            {
                return this.artWorks;
            }

            set
            {
                this.artWorks = value;
            }
        }
    }
}
