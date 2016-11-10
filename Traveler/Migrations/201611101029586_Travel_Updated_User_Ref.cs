namespace Traveler.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Travel_Updated_User_Ref : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Travels",
                c => new
                    {
                        TravelID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        UserID = c.String(),
                    })
                .PrimaryKey(t => t.TravelID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Travels");
        }
    }
}
