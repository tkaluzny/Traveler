namespace Traveler.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class city_in_travel1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Travels", "CityID", "dbo.Cities");
            DropForeignKey("dbo.Travels", "Country_CountryID", "dbo.Countries");
            DropIndex("dbo.Travels", new[] { "CityID" });
            DropIndex("dbo.Travels", new[] { "Country_CountryID" });
            CreateTable(
                "dbo.TravelCities",
                c => new
                    {
                        Travel_TravelID = c.Int(nullable: false),
                        City_CityID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Travel_TravelID, t.City_CityID })
                .ForeignKey("dbo.Travels", t => t.Travel_TravelID, cascadeDelete: true)
                .ForeignKey("dbo.Cities", t => t.City_CityID, cascadeDelete: true)
                .Index(t => t.Travel_TravelID)
                .Index(t => t.City_CityID);
            
            DropColumn("dbo.Travels", "CityID");
            DropColumn("dbo.Travels", "Country_CountryID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Travels", "Country_CountryID", c => c.Int());
            AddColumn("dbo.Travels", "CityID", c => c.Int(nullable: false));
            DropForeignKey("dbo.TravelCities", "City_CityID", "dbo.Cities");
            DropForeignKey("dbo.TravelCities", "Travel_TravelID", "dbo.Travels");
            DropIndex("dbo.TravelCities", new[] { "City_CityID" });
            DropIndex("dbo.TravelCities", new[] { "Travel_TravelID" });
            DropTable("dbo.TravelCities");
            CreateIndex("dbo.Travels", "Country_CountryID");
            CreateIndex("dbo.Travels", "CityID");
            AddForeignKey("dbo.Travels", "Country_CountryID", "dbo.Countries", "CountryID");
            AddForeignKey("dbo.Travels", "CityID", "dbo.Cities", "CityID", cascadeDelete: true);
        }
    }
}
