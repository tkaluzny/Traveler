namespace Traveler.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class city_in_travel_2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Travels", "Country_CountryID", c => c.Int());
            CreateIndex("dbo.Travels", "Country_CountryID");
            AddForeignKey("dbo.Travels", "Country_CountryID", "dbo.Countries", "CountryID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Travels", "Country_CountryID", "dbo.Countries");
            DropIndex("dbo.Travels", new[] { "Country_CountryID" });
            DropColumn("dbo.Travels", "Country_CountryID");
        }
    }
}
