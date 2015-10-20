namespace ArtGallery.SqlServerModels.People
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Exhibits;
    using Places;

    public class Artist
    {
        private ICollection<ArtWork> artWorks;

        public Artist()
        {
            this.artWorks = new HashSet<ArtWork>();
        }

        public int Id { get; set; }

        [MaxLength(100)]
        public string FirstName { get; set; }

        [MaxLength(100)]
        public string MiddleName { get; set; }

        [MaxLength(100)]
        public string LastName { get; set; }

        [Column(TypeName = "DateTime2")]
        public DateTime DateOfBirth { get; set; }

        public int CountryId { get; set; }

        public virtual Country Country { get; set; }

        public virtual ICollection<ArtWork> ArtWorks
        {
            get { return this.artWorks; }

            set { this.artWorks = value; }
        }
    }
}
