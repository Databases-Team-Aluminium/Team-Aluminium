namespace ArtGallery.MySqlModel.Reports
{
    using System.ComponentModel.DataAnnotations;

    using Newtonsoft.Json;

    public class ArtWorkJsonReport
    {
        [Key]
        [JsonProperty(PropertyName = "art-work-id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "art-work-name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "art-work-type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "art-work-status")]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "art-work-income-value")]
        public decimal Income { get; set; }
    }
}
