namespace CreateJsonReports
{
    using System;
    using System.IO;
    using System.Linq;

    using ArtGallery.EntityFrameworkData;
    using ArtGallery.Models.Exhibits;
    using Newtonsoft.Json;

    public class JsonReportsGenerator
    {
        private static JsonReportsGenerator instance;

        public static JsonReportsGenerator Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new JsonReportsGenerator();
                }

                return instance;
            }
        }

        private JsonReportsGenerator()
        {
        }

        public void Run()
        {
            // To be refactored. Not really HQC
            Console.WriteLine("Generating JSON reports...");
            var ctx = new ArtGalleryDbContext();
            this.GetArtWorkReport(ctx);
            Console.WriteLine("Done.");
        }

        private void GetArtWorkReport(ArtGalleryDbContext ctx)
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
                    this.SaveReport(report, report.Id);
                }
            }
        }

        private void SaveReport(ArtWork report, int id)
        {
            var jsonReport = JsonConvert.SerializeObject(report, Formatting.Indented);
            File.WriteAllText("../../../Reports/JSON/" + id + ".json", jsonReport.ToString());
        }
    }
}
