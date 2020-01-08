namespace writeMeEverythingASP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addUserProp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Verify", c => c.Boolean(nullable: false));
            AddColumn("dbo.Users", "VerifyText", c => c.String());
            AddColumn("dbo.Users", "Lastseen", c => c.DateTime(nullable: false));
            AddColumn("dbo.Users", "isOnline", c => c.Boolean(nullable: false));
            AddColumn("dbo.Users", "ResetText", c => c.String());
            AddColumn("dbo.Users", "Avatar", c => c.String());
            AddColumn("dbo.Users", "About", c => c.String(maxLength: 1000));
            AddColumn("dbo.Users", "Phone", c => c.String(maxLength: 10));
            AddColumn("dbo.Users", "City", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "City");
            DropColumn("dbo.Users", "Phone");
            DropColumn("dbo.Users", "About");
            DropColumn("dbo.Users", "Avatar");
            DropColumn("dbo.Users", "ResetText");
            DropColumn("dbo.Users", "isOnline");
            DropColumn("dbo.Users", "Lastseen");
            DropColumn("dbo.Users", "VerifyText");
            DropColumn("dbo.Users", "Verify");
        }
    }
}
