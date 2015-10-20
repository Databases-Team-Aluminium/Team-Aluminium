namespace ArtGallery.MongoDbModels.People
{
    using Common;
    using Contracts;

    public class Artist : Person
    {
        public ArtStyle Style { get; set; }
    }
}

