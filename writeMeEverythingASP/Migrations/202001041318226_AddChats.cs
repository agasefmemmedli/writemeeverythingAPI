namespace writeMeEverythingASP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddChats : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Chats",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreateAt = c.DateTime(nullable: false),
                        SenderId = c.Int(nullable: false),
                        ReceiverId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.ReceiverId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.SenderId, cascadeDelete: false)
                .Index(t => t.SenderId)
                .Index(t => t.ReceiverId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Chats", "SenderId", "dbo.Users");
            DropForeignKey("dbo.Chats", "ReceiverId", "dbo.Users");
            DropIndex("dbo.Chats", new[] { "ReceiverId" });
            DropIndex("dbo.Chats", new[] { "SenderId" });
            DropTable("dbo.Chats");
        }
    }
}
