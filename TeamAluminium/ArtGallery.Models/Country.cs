namespace ArtGallery.Models
{
    using System.Collections.Generic;

    public class Country
    {
        private ICollection<Person> population;

        public Country()
        {
            this.population = new HashSet<Person>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Person> Population
        {
            get
            {
                return this.population;
            }

            set
            {
                this.population = value;
            }
        }
    }
}
