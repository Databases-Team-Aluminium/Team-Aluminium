namespace GeneratePdfReports
{
    using System.IO;
    using System.Linq;
    using ArtGaller.EntityFrameworkData;
    using iTextSharp.text;
    using iTextSharp.text.pdf;

    public class Program
    {
        public static void Main()
        {
            // Needs refactoring and some fixes with the first date
            var db = new ArtGalleryDbContext();
            var dataCount = db.ArtWorks.Select(a => a.DateSold).Distinct().ToList();

            Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);
            PdfWriter.GetInstance(doc, new FileStream("../../../Reports/PDF/artworks.pdf", FileMode.Create));
            doc.Open();

            PdfPTable titleHeader = new PdfPTable(1);
            PdfPCell cellHeader = new PdfPCell(new Phrase("Aggregated Artworks Report"));
            cellHeader.HorizontalAlignment = 1;
            titleHeader.AddCell(cellHeader);
            doc.Add(titleHeader);

            decimal grandTotalSum = 0;
            foreach (var date in dataCount)
            {
                PdfPTable tableHeader = new PdfPTable(5);
                {
                    PdfPCell cellHeaderDate = new PdfPCell(new Phrase("Date: " + date.ToShortDateString()));
                    cellHeaderDate.Colspan = 5;
                    cellHeaderDate.BackgroundColor = new BaseColor(217, 217, 217);

                    PdfPCell artist = new PdfPCell(new Phrase("Artist"));
                    artist.BackgroundColor = new BaseColor(217, 217, 217);

                    PdfPCell title = new PdfPCell(new Phrase("Title"));
                    title.BackgroundColor = new BaseColor(217, 217, 217);

                    PdfPCell type = new PdfPCell(new Phrase("Artwork type"));
                    type.BackgroundColor = new BaseColor(217, 217, 217);

                    PdfPCell status = new PdfPCell(new Phrase("Status"));
                    status.BackgroundColor = new BaseColor(217, 217, 217);

                    PdfPCell price = new PdfPCell(new Phrase("Price"));
                    price.BackgroundColor = new BaseColor(217, 217, 217);

                    tableHeader.AddCell(cellHeaderDate);
                    tableHeader.AddCell(artist);
                    tableHeader.AddCell(title);
                    tableHeader.AddCell(type);
                    tableHeader.AddCell(status);
                    tableHeader.AddCell(price);

                }

                decimal totalSum = 0;


                PdfPTable tableBody = new PdfPTable(5);
                {
                    var artworks = db.ArtWorks.Where(a => a.DateSold == date).ToList();
                    tableBody.DefaultCell.HorizontalAlignment = 1;

                    foreach (var artwork in artworks)
                    {
                        tableBody.AddCell(db.ArtWorks.Find(artwork.ArtistId).Artist.FirstName + " " + db.ArtWorks.Find(artwork.ArtistId).Artist.LastName);
                        tableBody.AddCell(artwork.Title);
                        tableBody.AddCell(artwork.Type.ToString());
                        tableBody.AddCell(artwork.Status.ToString());
                        tableBody.AddCell(artwork.Value.ToString());

                        totalSum += artwork.Value;
                    }
                    grandTotalSum += totalSum;
                }

                PdfPTable tableFooter = new PdfPTable(5);
                {
                    PdfPCell totalSumTextCell = new PdfPCell(new Phrase("Total sum for " + date.ToShortDateString() + ": "));
                    totalSumTextCell.Colspan = 4;
                    totalSumTextCell.HorizontalAlignment = 2;
                    tableFooter.AddCell(totalSumTextCell);

                    PdfPCell totalSumCell = new PdfPCell(new Phrase(totalSum.ToString()));
                    totalSumCell.HorizontalAlignment = 2;
                    tableFooter.AddCell(totalSumCell);
                }

                doc.Add(tableHeader);
                doc.Add(tableBody);
                doc.Add(tableFooter);
            }

            PdfPTable footer = new PdfPTable(5);

            PdfPCell grandSumTextCell = new PdfPCell(new Phrase("Grand total: "));
            grandSumTextCell.Colspan = 4;
            grandSumTextCell.HorizontalAlignment = 2;
            footer.AddCell(grandSumTextCell);

            PdfPCell grandSumCell = new PdfPCell(new Phrase(grandTotalSum.ToString()));
            grandSumCell.HorizontalAlignment = 2;
            footer.AddCell(grandSumCell);

            doc.Add(footer);
            doc.Close();
        }
    }
}
