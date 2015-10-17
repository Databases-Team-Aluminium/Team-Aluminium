namespace ArtGallery.Models.MongoDbModels
{
    public class Employee : Person
    {
        public string EmployeeIdentifier { get; set; }

        public decimal YearSalary { get; set; }

        public int DepartmentId { get; set; }
    }
}
