namespace ArtGallery.Models.MongoDbModels
{
    using System.Collections.Generic;

    public class Artist : Person
    {
        private ICollection<ArtWork> artWorks;

        public Artist()
        {
            this.artWorks = new HashSet<ArtWork>();
        }

        public ICollection<ArtWork> ArtWorks
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
