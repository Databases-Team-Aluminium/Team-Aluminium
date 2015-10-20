namespace CreateJsonReports.JsonManagers
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using ArtGallery.SqlServerData;
    using ArtGallery.MySqlData;
    using ArtGallery.MySqlModel.Reports;
    using Common;
    using Contracts;
    using Newtonsoft.Json;

    public class JsonManager : IManager, IObservable
    {
        private const string PathToJsonReports = "../../../Output/Json-Reports";
        private const string ConnetionString = "mongodb://localhost:27017";
        private const string DbName = "artgallerydb";


        private readonly ICollection<IObserver> subscribers;
        private Notification state;

        public JsonManager()
        {
            this.subscribers = new List<IObserver>();
        }

        public void WriteData()
        {
            this.ChangeState(new Notification
            {
                Message = "Generating JSON reports and Importing them into MySql..."
            });

            using (var db = new ArtGalleryDbContext())
            {
                var artWorkReports = db.ArtWorks.Select(a => new ArtWorkJsonReport
                {
                    Id = a.Id,
                    Name = a.Title,
                    Type = a.Type.ToString(),
                    Status = a.Status.ToString(),
                    Income = a.DateSold.HasValue ? a.Value : 0
                })
                .AsEnumerable();

                var counter = 0;
                var mySql = new ArtGalleryMySqlDbContext();
                foreach (var report in artWorkReports)
                {
                    this.SaveAndWriteReport(report, report.Id);
                    mySql.Reports.Add(report);
                    mySql.SaveChanges();
                    counter += 1;
                    if (counter == 20)
                    {
                        mySql.Dispose();
                        mySql = new ArtGalleryMySqlDbContext();
                    }
                }

            }

            this.ChangeState(new Notification
            {
                Message = "Done."
            });
        }


        public void Subscribe(IObserver observer)
        {
            this.subscribers.Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            this.subscribers.Remove(observer);
        }

        public void ChangeState(Notification notification)
        {
            this.state = notification;
            this.Notify();
        }

        public void Notify()
        {
            foreach (IObserver subscriber in this.subscribers)
            {
                subscriber.Update(this.state);
            }
        }

        private void SaveAndWriteReport(ArtWorkJsonReport report, int id)
        {
            string jsonReport = JsonConvert.SerializeObject(report, Formatting.Indented);
            File.WriteAllText(PathToJsonReports + "/" + id + ".json", jsonReport.ToString());
        }
    }
}
