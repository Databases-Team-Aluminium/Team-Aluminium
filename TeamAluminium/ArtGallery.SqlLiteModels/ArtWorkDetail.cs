namespace ArtGallery.SqlLiteModels
{
    public class ArtWorkDetails
    {
        public int Id { get; set; }
        
        public string ArtWorkName { get; set; }

        /// <summary>
        /// Weight in kilograms
        /// </summary>
        public decimal Weight { get; set; }

        public int NumberOfLayersOfMaterial { get; set; }
    }
}
