namespace LoadXmlIntoMongoDbAndMsSql.Models
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public class ArtWorkInformationRoot
    {
        [XmlElement("art-work")]
        public HashSet<ArtWorkInformation> Collection { get; set; }
    }
}
