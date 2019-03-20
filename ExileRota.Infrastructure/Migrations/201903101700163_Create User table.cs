namespace ExileRota.Infrastructure.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class CreateUsertable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                    "dbo.Users",
                    c => new
                    {
                        UserId = c.Guid(nullable: false),
                        Email = c.String(nullable: false),
                        Username = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        Salt = c.String(nullable: false),
                        Ign = c.String(nullable: false),
                        Role = c.String(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);

        }

        public override void Down()
        {
            DropTable("dbo.Users");
        }
    }
}