﻿namespace ArtGallery.MongoDbModels.XmlModels
{
    using System.Xml.Serialization;

    [XmlType(AnonymousType = true)]
    public class XmlArtist
    {
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string DateOfBird { get; set; }
    }
}
