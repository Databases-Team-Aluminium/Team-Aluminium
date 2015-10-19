namespace LoadXmlIntoMongoDbAndMsSql
{
    using Writers;
    using DataImporters;
    using System;
    using DataLoaders;

    public class MongoDbAndMsSqlXmlDataImporter
    {
        private static MongoDbAndMsSqlXmlDataImporter instance;

        private MongoDbAndMsSqlXmlDataImporter()
        {
        }

        public static MongoDbAndMsSqlXmlDataImporter Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MongoDbAndMsSqlXmlDataImporter();
                }

                return instance;
            }
        }

        public void Run()
        {
            var textFileLoader = new XmlDataLoader();
            var consoleWriter = new TextWriter(Console.Out);
            var importer = new MongoDbDataImporter(textFileLoader);

            importer.Subscribe(consoleWriter);
            importer.ImportData();
        }
    }
}
