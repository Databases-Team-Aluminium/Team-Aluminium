namespace ArtGallery.Models.XmlModels
{
    using System.Xml.Serialization;

    [XmlTypeAttribute(AnonymousType = true)]
    public class XmlArtWork
    {
        public string Title { get; set; }

        public byte Type { get; set; }

        public byte Status { get; set; }
    }
}
