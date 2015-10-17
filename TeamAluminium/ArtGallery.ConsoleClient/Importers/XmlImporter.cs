namespace ArtGallery.ConsoleClient.Importers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml.Serialization;

    using ArtGallery.Models.MongoDbModels;
    using ArtGallery.Models.XmlModels;
    using ArtGallery.Models.Common;

    public class XmlImporter : IImporter
    {
        public const string ArtistFilePAth = "../../Data/Xml/Artists.xml";
        public const string ArtWorkFilePAth = "../../Data/Xml/ArtWorks.xml";

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

        public ICollection<ArtWork> GetArtworks()
        {
            var xmlArtWork = this.GetXmlCollection<XmlArtworkRoot>(ArtWorkFilePAth, "ArtWorks");

            var result = xmlArtWork
                .ArtWorks
                .Select(x => new ArtWork
                {
                    Title = x.Title,
                    Type = (ArtWorkType) x.Type,
                    Status = (ArtWorkStatus) x.Status
                })
                .ToList();

            return result;
        }

        private DateTime ParseDate(string date)
        {
            var splitedDate = date.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            var year = int.Parse(splitedDate[2]);
            var day = int.Parse(splitedDate[1]);
            var month = int.Parse(splitedDate[0]);

            return new DateTime(year, month, day);
        }

        private  T GetXmlCollection<T>(string path, string rootElement)
        {
            T result;

            XmlSerializer serializer = new XmlSerializer(typeof(T), new XmlRootAttribute(rootElement));

            using (var reader = new StreamReader(path))
            {
                result = (T)serializer.Deserialize(reader);
            }

            return result;
        }
    }
}
