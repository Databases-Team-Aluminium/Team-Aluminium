namespace CreateJsonReports
{
    using System.IO;
    using System.Linq;

    using ArtGallery.SqlServerData;
    using Newtonsoft.Json;
    using ArtGallery.Setup.Common;
    using System.Collections.Generic;
    using ArtGallery.MySqlData;
    using ArtGallery.MySqlModel.Reports;
    using Writers;
    using System;
    using ArtGallery.Setup.Writers.Contracts;
    using JsonManagers;

    public class JsonReportsGenerator
    {
        private static JsonReportsGenerator instance;

        public static JsonReportsGenerator Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new JsonReportsGenerator();
                }

                return instance;
            }
        }

        private JsonReportsGenerator()
        {
        }

        public void Run()
        {
            var writer = new Writers.TextWriter(Console.Out);
            var manager = new JsonManager();
            manager.Subscribe(writer);

            manager.WriteData();
        }
    }
}

