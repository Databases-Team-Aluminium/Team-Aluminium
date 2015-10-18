namespace LoadXmlIntoMongoDbAndMsSql
{
    using Writers;
    using DataImporters;
    using System;
    using DataLoaders;

    public class Client
    {
        private static Client instance;

        private Client()
        {
        }

        public static Client Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Client();
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
