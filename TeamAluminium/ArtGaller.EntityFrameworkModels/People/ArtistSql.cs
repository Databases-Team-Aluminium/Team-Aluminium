﻿namespace ArtGallery.EntityFrameworkModels.People
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using ArtGallery.EntityFrameworkModels.Exhibits;
    using ArtGallery.EntityFrameworkModels.Places;

    public class ArtistSql
    {
        private ICollection<ArtWorkSql> artWorks;

        public ArtistSql()
        {
            this.artWorks = new HashSet<ArtWorkSql>();
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

        public virtual CountrySql Country { get; set; }

        public virtual ICollection<ArtWorkSql> ArtWorks
        {
            get { return this.artWorks; }

            set { this.artWorks = value; }
        }
    }
}
