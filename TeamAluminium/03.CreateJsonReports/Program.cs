namespace CreateJsonReports
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using ArtGallery.EntityFrameworkData;
    using ArtGallery.EntityFrameworkModels.Exhibits;
    using ArtGallery.Models.Exhibits;
    using Newtonsoft.Json;

    public class Program
    {
        public static void Main()
        {
            // To be refactored. Not really HQC
            System.Console.WriteLine("Started...");
            var ctx = new ArtGalleryDbContext();
            GetArtWorkReport(ctx);
            System.Console.WriteLine("Done");
        }

        private static void GetArtWorkReport(ArtGalleryDbContext ctx)
        {
            using (ctx)
            {
               var artWorkReports = ctx.ArtWorks.Select(a => new ArtWork()
                {
                    Id = a.Id,
                    Title = a.Title,
                    Type = a.Type,
                    Status = a.Status,
                    ArtistId = a.ArtistId,
                    Value = a.Value,
                    DateSold = a.DateSold
                }).ToList();

                foreach (var report in artWorkReports)
                {
                    SaveReport(report, report.Id);
                }
            }
        }

        private static void SaveReport(ArtWork report, int id)
        {
            var jsonReport = JsonConvert.SerializeObject(report, Formatting.Indented);
            File.WriteAllText("../../../Reports/JSON/" + id + ".json", jsonReport.ToString());
        }

    }
}
