namespace LoadXmlIntoMongoDbAndMsSql.DataLoaders.Contracts
{
    using System.Collections.Generic;

    using Models;

    public interface IDataLoader
    {
        IEnumerable<ArtWorkInformation> GetArtWorksInformation();
    }
}
