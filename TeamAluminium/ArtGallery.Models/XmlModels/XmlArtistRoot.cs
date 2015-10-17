namespace ArtGallery.Models.XmlModels
{
    using System.Xml.Serialization;

    [XmlTypeAttribute(AnonymousType = true)]
    [XmlRootAttribute(Namespace = "", IsNullable = false)]
    public class XmlArtistRoot
    {
        [XmlElementAttribute("Atritst")]
        public XmlArtist[] Atritsts { get; set; }
    }
}
