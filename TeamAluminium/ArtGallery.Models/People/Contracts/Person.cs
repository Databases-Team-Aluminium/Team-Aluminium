namespace ArtGallery.Models.People.Contracts
{
    using System;

    public class Person
    {
        public int Id { get; set; }
        
        public string FirstName { get; set; }
        
        public string MiddleName { get; set; }
        
        public string LastName { get; set; }
        
        public DateTime DateOfBirth { get; set; }
        
        public int CountryId { get; set; }
    }
}