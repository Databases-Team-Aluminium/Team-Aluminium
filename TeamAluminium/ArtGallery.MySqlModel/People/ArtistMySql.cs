namespace ArtGallery.MySqlModel.People
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using ArtGallery.MySqlModel.Exhibits;
    using ArtGallery.MySqlModel.Places;

    public class ArtistMySql
    {
        private ICollection<ArtWorkMySql> artWorks;

        public ArtistMySql()
        {
            this.artWorks = new HashSet<ArtWorkMySql>();
        }

        public int Id { get; set; }

        [MaxLength(100)]
        public string FirstName { get; set; }

        [MaxLength(100)]
        public string MiddleName { get; set; }

        [MaxLength(100)]
        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public int CountryId { get; set; }

        public virtual CountryMySql Country { get; set; }

        public virtual ICollection<ArtWorkMySql> ArtWorks
        {
            get { return this.artWorks; }

            set { this.artWorks = value; }
        }
    }
}
