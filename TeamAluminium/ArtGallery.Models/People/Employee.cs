namespace ArtGallery.MongoDbModels.People
{
    using Contracts;

    public class Employee : Person
    {
        public string EmployeeIdentifier { get; set; }

        public decimal YearSalary { get; set; }

        public int DepartmentId { get; set; }
    }
}
