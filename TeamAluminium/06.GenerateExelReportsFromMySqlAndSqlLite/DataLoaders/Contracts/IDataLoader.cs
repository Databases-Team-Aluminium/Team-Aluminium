namespace GenerateExcelReportFromMySqlAndSqLite.DataLoaders.Contracts
{
    using System.Collections.Generic;

    public interface IDataLoader
    {
        ICollection<string> GetArtworks();
    }
}
