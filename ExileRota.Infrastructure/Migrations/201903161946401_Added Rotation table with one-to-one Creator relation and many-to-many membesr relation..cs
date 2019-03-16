namespace ExileRota.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedRotationtablewithonetooneCreatorrelationandmanytomanymembesrrelation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Rotations",
                c => new
                    {
                        RotationId = c.Guid(nullable: false),
                        Creator = c.Guid(nullable: false),
                        League = c.String(),
                        Type = c.String(),
                        Spots = c.Int(),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.RotationId)
                .ForeignKey("dbo.Users", t => t.Creator, cascadeDelete: true)
                .Index(t => t.Creator);
            
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        RotationID = c.Guid(nullable: false),
                        UserID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.RotationID, t.UserID })
                .ForeignKey("dbo.Rotations", t => t.RotationID)
                .ForeignKey("dbo.Users", t => t.UserID)
                .Index(t => t.RotationID)
                .Index(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rotations", "Creator", "dbo.Users");
            DropForeignKey("dbo.Members", "UserID", "dbo.Users");
            DropForeignKey("dbo.Members", "RotationID", "dbo.Rotations");
            DropIndex("dbo.Members", new[] { "UserID" });
            DropIndex("dbo.Members", new[] { "RotationID" });
            DropIndex("dbo.Rotations", new[] { "Creator" });
            DropTable("dbo.Members");
            DropTable("dbo.Rotations");
        }
    }
}
