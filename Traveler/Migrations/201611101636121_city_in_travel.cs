namespace Traveler.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class city_in_travel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Travels", "CityID", c => c.Int(nullable: false));
            CreateIndex("dbo.Travels", "CityID");
            AddForeignKey("dbo.Travels", "CityID", "dbo.Cities", "CityID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Travels", "CityID", "dbo.Cities");
            DropIndex("dbo.Travels", new[] { "CityID" });
            DropColumn("dbo.Travels", "CityID");
        }
    }
}
