namespace ArtGallery.Models.XmlModels
{
    using System.Xml.Serialization;

    [XmlTypeAttribute(AnonymousType = true)]
    [XmlRootAttribute(Namespace = "", IsNullable = false)]
    public class XmlArtworkRoot
    {

        [XmlElementAttribute("ArtWork")]
        public XmlArtWork[] ArtWorks { get; set; }
    }
}
