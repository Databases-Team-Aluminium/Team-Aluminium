namespace ArtGallery.EntityFrameworkModels.People
{
    using System;
    using System.Collections.Generic;

    using ArtGallery.EntityFrameworkModels.Exhibits;
    using ArtGallery.EntityFrameworkModels.Places;

    public class ArtistSql
    {
        private ICollection<ArtWorkSql> artWork;

        public ArtistSql()
        {
            this.artWork = new HashSet<ArtWorkSql>();
        }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public int CountryId { get; set; }

        public virtual CountrySql Country { get; set; }

        public virtual ICollection<ArtWorkSql> ArtWork
        {
            get { return this.artWork; }

            set { this.artWork = value; }
        }

    }
}

