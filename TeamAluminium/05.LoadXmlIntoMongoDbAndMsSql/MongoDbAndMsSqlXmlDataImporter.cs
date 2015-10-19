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
            var mongoDbImporter = new MongoDbDataImporter(textFileLoader);
            var msSqlDbImporter = new MsSqlDataImporter(textFileLoader);

            mongoDbImporter.Subscribe(consoleWriter);
            msSqlDbImporter.Subscribe(consoleWriter);
            mongoDbImporter.ImportData();
            msSqlDbImporter.ImportData();
        }
    }
}
