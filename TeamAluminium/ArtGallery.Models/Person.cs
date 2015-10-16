namespace ArtGallery.Models
{
    using System;

    public class Person
    {
        public int Id { get; set; }
        
        public string FirstName { get; set; }
        
        public string MiddleName { get; set; }
        
        public string LastName { get; set; }
        
        public string Nationality { get; set; }

        public DateTime? DateOfBirth { get; set; }
    }
}