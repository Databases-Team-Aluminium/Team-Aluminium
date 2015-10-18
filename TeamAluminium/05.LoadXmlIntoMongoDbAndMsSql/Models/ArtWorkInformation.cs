namespace LoadXmlIntoMongoDbAndMsSql.Models
{
    using System.Xml.Serialization;

    [XmlType(AnonymousType = true)]
    public class ArtWorkInformation
    {
        [XmlElement("id")]
        public int Id { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }
    }
}
