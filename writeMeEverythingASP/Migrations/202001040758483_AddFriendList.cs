namespace writeMeEverythingASP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFriendList : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Friends",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SenderId = c.Int(nullable: false),
                        AcceptorId = c.Int(nullable: false),
                        isFriend = c.Boolean(nullable: false),
                        isSenderBlocked = c.Boolean(nullable: false),
                        isAcceptorBlocked = c.Boolean(nullable: false),
                        CreateAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.AcceptorId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.SenderId, cascadeDelete: false)
                .Index(t => t.SenderId)
                .Index(t => t.AcceptorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Friends", "SenderId", "dbo.Users");
            DropForeignKey("dbo.Friends", "AcceptorId", "dbo.Users");
            DropIndex("dbo.Friends", new[] { "AcceptorId" });
            DropIndex("dbo.Friends", new[] { "SenderId" });
            DropTable("dbo.Friends");
        }
    }
}
