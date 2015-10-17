namespace ArtGallery.Models.XmlModels
{
    using System.Xml.Serialization;

    [XmlTypeAttribute(AnonymousType = true)]
    public class XmlArtist
    {
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string DateOfBird { get; set; }
    }
}
