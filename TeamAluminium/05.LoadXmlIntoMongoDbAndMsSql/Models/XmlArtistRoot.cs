namespace ArtGallery.MongoDbModels.XmlModels
{
    using System.Xml.Serialization;

    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public class XmlArtistRoot
    {
        [XmlElement("Atritst")]
        public XmlArtist[] Atritsts { get; set; }
    }
}
