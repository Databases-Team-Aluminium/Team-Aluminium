namespace LoadXmlIntoMongoDbAndMsSql.DataLoaders
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml.Serialization;

    using Contracts;
    using ArtGallery.MongoDbModels.XmlModels;
    using ArtGallery.MongoDbModels.People;
    using Models;

    public class XmlDataLoader : IDataLoader
    {
        public const string ArtistFilePAth = "../../Data/Artists.xml";
        public const string ArtWorksInformationPath = "../../Data/Art-Works-Descriptions.xml";
        public const string ArtWorksInformationRootElementName = "art-works-descriptions";

        public ICollection<Artist> GetArtists()
        {
            var xmlArtists = this.GetXmlCollection<XmlArtistRoot>(ArtistFilePAth, "Artists");

            var result = xmlArtists
                .Atritsts
                .Select(x => new Artist
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    MiddleName = x.MiddleName,
                    DateOfBirth = this.ParseDate(x.DateOfBird)
                })
                .ToList();

            return result;

        }

        public IEnumerable<ArtWorkInformation> GetArtWorksInformation()
        {
            IEnumerable<ArtWorkInformation> xmlArtWorksInfos = this
                .GetXmlCollection<ArtWorkInformationRoot>
                (
                    ArtWorksInformationPath,
                    ArtWorksInformationRootElementName
                )
                .Collection;

            return xmlArtWorksInfos;
        }

        private DateTime ParseDate(string date)
        {
            string[] splitDate = date.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            int year = int.Parse(splitDate[2]);
            int day = int.Parse(splitDate[1]);
            int month = int.Parse(splitDate[0]);

            return new DateTime(year, month, day);
        }

        private T GetXmlCollection<T>(string path, string rootElement)
        {
            T result;
            var serializer = new XmlSerializer(typeof(T), new XmlRootAttribute(rootElement));
            using (var reader = new StreamReader(path))
            {
                result = (T)serializer.Deserialize(reader);
            }

            return result;
        }
    }
}