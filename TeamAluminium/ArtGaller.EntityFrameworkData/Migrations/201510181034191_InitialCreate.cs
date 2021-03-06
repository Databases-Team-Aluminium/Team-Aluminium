namespace ArtGallery.SqlServerData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ArtistSqls",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        MiddleName = c.String(),
                        LastName = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                        CountryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CountrySqls", t => t.CountryId, cascadeDelete: true)
                .Index(t => t.CountryId);

            CreateTable(
                "dbo.ArtWorkSqls",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Type = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        ArtistId = c.Int(nullable: false),
                        Value = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DateSold = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ArtistSqls", t => t.ArtistId, cascadeDelete: true)
                .Index(t => t.ArtistId);

            CreateTable(
                "dbo.CountrySqls",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
        }

        public override void Down()
        {
            this.DropForeignKey("dbo.ArtistSqls", "CountryId", "dbo.CountrySqls");
            this.DropForeignKey("dbo.ArtWorkSqls", "ArtistId", "dbo.ArtistSqls");
            this.DropIndex("dbo.ArtWorkSqls", new[] { "ArtistId" });
            this.DropIndex("dbo.ArtistSqls", new[] { "CountryId" });
            this.DropTable("dbo.CountrySqls");
            this.DropTable("dbo.ArtWorkSqls");
            this.DropTable("dbo.ArtistSqls");
        }
    }
}
