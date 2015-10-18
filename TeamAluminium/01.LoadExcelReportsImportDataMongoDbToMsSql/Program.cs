namespace LoadExcelReportsImportDataMongoDbToMsSql
{
    using ArtGaller.EntityFrameworkData;
    using ArtGallery.EntityFrameworkModels.People;

    public class Program
    {
        public static void Main()
        {
            var db = new ArtGalleryDbContext();

            db.Artists.Add(new ArtistSql
            {
                FirstName = "Pesho",
                MiddleName = "d",
                LastName = "Gosho"
            });

        }
    }
}
