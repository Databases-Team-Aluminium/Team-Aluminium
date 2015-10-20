namespace GenerateExcelReportFromMySqlAndSqLite.DataLoaders
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using Contracts;

    public class TextFileLoader : IDataLoader
    {
        public ICollection<string> GetArtworks()
        {
            string[] artWorks = File
                .ReadAllText(path: "../../Data/ArtWorkNames.txt")
                .Split(new char[] { '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries);

            return artWorks;
        }
    }
}