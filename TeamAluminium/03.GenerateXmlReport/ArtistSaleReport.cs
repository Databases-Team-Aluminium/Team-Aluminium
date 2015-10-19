namespace GenerateXmlReport
{
    using System;

    public class ArtistSaleReport
    {
        public int ArtistId { get; set; }

        public string FullName { get; set; }

        public DateTime DateSold { get; set; }

        public decimal Price { get; set; }
    }
}
