namespace writeMeEverythingASP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateMessageTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ChatId = c.Int(nullable: false),
                        Content = c.String(nullable: false),
                        CreateAt = c.DateTime(nullable: false),
                        SenderId = c.Int(nullable: false),
                        ReceiverId = c.Int(nullable: false),
                        isDeletedFromSender = c.Boolean(nullable: false),
                        isDeletedFromReceiver = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Chats", t => t.ChatId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.ReceiverId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.SenderId, cascadeDelete: false)
                .Index(t => t.ChatId)
                .Index(t => t.SenderId)
                .Index(t => t.ReceiverId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Messages", "SenderId", "dbo.Users");
            DropForeignKey("dbo.Messages", "ReceiverId", "dbo.Users");
            DropForeignKey("dbo.Messages", "ChatId", "dbo.Chats");
            DropIndex("dbo.Messages", new[] { "ReceiverId" });
            DropIndex("dbo.Messages", new[] { "SenderId" });
            DropIndex("dbo.Messages", new[] { "ChatId" });
            DropTable("dbo.Messages");
        }
    }
}
