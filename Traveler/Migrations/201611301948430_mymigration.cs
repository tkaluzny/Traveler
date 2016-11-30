namespace Traveler.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mymigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        travel_TravelID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Travels", t => t.travel_TravelID)
                .Index(t => t.travel_TravelID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Images", "travel_TravelID", "dbo.Travels");
            DropIndex("dbo.Images", new[] { "travel_TravelID" });
            DropTable("dbo.Images");
        }
    }
}
