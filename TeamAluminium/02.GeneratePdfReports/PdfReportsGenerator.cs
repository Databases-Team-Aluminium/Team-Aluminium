namespace GeneratePdfReports
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using ArtGallery.EntityFrameworkData;
    using iTextSharp.text;
    using iTextSharp.text.pdf;

    public class PdfReportsGenerator
    {
        private static PdfReportsGenerator instance;

        public static PdfReportsGenerator Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PdfReportsGenerator();
                }

                return instance;
            }
        }

        private PdfReportsGenerator()
        {
            
        }
        public void Run()
        {
            // Needs refactoring
            Console.WriteLine("Generating PDF reports...");

            var db = new ArtGalleryDbContext();
            List<int?> yearsOfSales = db
                .ArtWorks
                .Select(a => a.DateSold)
                .Distinct()
                .AsEnumerable()
                .Select<DateTime?, int?>(d =>
                {
                    if (d.HasValue)
                    {
                        return d.GetValueOrDefault().Year;
                    }

                    return null;
                })
                .Distinct()
                .ToList();

            var doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);
            PdfWriter.GetInstance(doc, new FileStream("../../../Reports/PDF/Yearly-Artworks-Sales-Report.pdf", FileMode.Create));
            doc.Open();

            PdfPTable titleHeader = new PdfPTable(1);
            PdfPCell cellHeader = new PdfPCell(new Phrase("Aggregated Yearly Artworks Sales Report"));
            cellHeader.HorizontalAlignment = 1;
            titleHeader.AddCell(cellHeader);
            doc.Add(titleHeader);

            decimal grandTotalSum = 0;
            decimal totalWorthOfSales = 0;
            decimal totalWorthLeft = 0;
            foreach (var date in yearsOfSales)
            {
                string dateGroup = string.Empty;
                if (!date.HasValue)
                {
                    dateGroup = "N/A"; // Not available, i.e not sold yet
                }
                else
                {
                    dateGroup = date.GetValueOrDefault().ToString();
                }

                PdfPTable tableHeader = new PdfPTable(5);
                {
                    PdfPCell cellHeaderDate = new PdfPCell(new Phrase("Year: " + dateGroup));
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
                    var artworks = db
                        .ArtWorks
                        .AsEnumerable()
                        .Where(a =>
                        {
                            if (date == null)
                            {
                                return !a.DateSold.HasValue;
                            }
                            
                            return a.DateSold.GetValueOrDefault().Year == date;
                        })
                        .ToList();

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
                    if (date.HasValue)
                    {
                        totalWorthOfSales += totalSum;
                    }
                }

                PdfPTable tableFooter = new PdfPTable(5);
                {
                    PdfPCell totalSumTextCell = new PdfPCell(new Phrase("Total sum for " +
                        dateGroup + ": "));
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

            totalWorthLeft = grandTotalSum - totalWorthOfSales;

            var footer = new PdfPTable(5);
            var grandSumText = new PdfPCell(new Phrase("Grand total: "));
            grandSumText.Colspan = 4;
            grandSumText.HorizontalAlignment = 2;

            var grandSum = new PdfPCell(new Phrase(grandTotalSum.ToString()));
            grandSum.HorizontalAlignment = 2;

            var totalSoldText = new PdfPCell(new Phrase("Total worth of sales: "));
            totalSoldText.Colspan = 4;
            totalSoldText.HorizontalAlignment = 2;

            var totalSoldValue = new PdfPCell(new Phrase(totalWorthOfSales.ToString()));
            totalSoldValue.HorizontalAlignment = 2;

            var totalRemainingText = new PdfPCell(new Phrase("Total worth remaining: "));
            totalRemainingText.Colspan = 4;
            totalRemainingText.HorizontalAlignment = 2;

            var totalRemainingValue = new PdfPCell(new Phrase(totalWorthLeft.ToString()));
            totalRemainingValue.HorizontalAlignment = 2;

            footer.AddCell(totalSoldText);
            footer.AddCell(totalSoldValue);
            footer.AddCell(totalRemainingText);
            footer.AddCell(totalRemainingValue);
            footer.AddCell(grandSumText);
            footer.AddCell(grandSum);

            doc.Add(footer);

            doc.Close();
            Console.WriteLine("Done.");
        }
    }
}
