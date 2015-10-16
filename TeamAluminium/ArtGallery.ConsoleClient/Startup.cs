using ArtGallery.ConsoleClient.Utils;

namespace ArtGallery.ConsoleClient
{
    public class Startup
    {
        public static void Main()
        {
            new MongoDbDataImporter().ImportSampleData();
            
        }
    }
}
