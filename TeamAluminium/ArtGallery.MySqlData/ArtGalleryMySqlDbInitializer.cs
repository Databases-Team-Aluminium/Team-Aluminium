using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace ArtGallery.MySqlData
{
    public class ArtGalleryMySqlDbInitializer : CreateDatabaseIfNotExists<ArtGalleryMySqlDbContext>
    {
        protected override void Seed(ArtGalleryMySqlDbContext data)
       {
           // create 3 students to seed the database
           //data.Students.Add(new Student { ID = 1, FirstName = "Mark", LastName = "Richards", EnrollmentDate = DateTime.Now });
           //data.Students.Add(new Student { ID = 2, FirstName = "Paula", LastName = "Allen", EnrollmentDate = DateTime.Now });
           //data.Students.Add(new Student { ID = 3, FirstName = "Tom", LastName = "Hoover", EnrollmentDate = DateTime.Now });
           //base.Seed(data);
       }
    }
}
