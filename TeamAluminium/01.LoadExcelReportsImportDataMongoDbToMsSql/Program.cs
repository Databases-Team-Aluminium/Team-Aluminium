﻿namespace LoadExcelReportsImportDataMongoDbToMsSql
{
    using System;
    using System.Data.Entity;
    using ArtGaller.EntityFrameworkData;
    using ArtGaller.EntityFrameworkData.Migrations;
    using ArtGallery.ConsoleClient.Writers;

    public class Program
    {
        public static void Main()
        {
            //string ConnetionString = "mongodb://localhost:27017";
            //string DbName = "artgallerydb";

            //var client = new MongoClient(ConnetionString);
            //MongoServer server = client.GetServer();
            //var mongoDb = server.GetDatabase(DbName);


            // var result = mongoDb.GetCollection<Artist>("artists").AsQueryable<Artist>();
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ArtGalleryDbContext, Configuration>());

            var data = new ArtGalleryDbContext();
            var dataImporter = new MongoDb();
            var consoleWriter = new TextWriter(Console.Out);

            var msSqlDbDataImporter = new MsSqlDbDataImporter(dataImporter, data);
            msSqlDbDataImporter.Subscribe(consoleWriter);
            msSqlDbDataImporter.ImportData();

            //db.Artists.Add(new ArtistSql
            //{
            //    FirstName = "Pesho",
            //    MiddleName = "d",
            //    LastName = "Gosho"
            //});


        }
    }
}
