namespace writeMeEverythingASP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteErrorMesssages : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "Token", c => c.String());
            DropTable("dbo.ErrorMessages");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ErrorMessages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ResName = c.String(),
                        Text_en = c.String(),
                        Text_ru = c.String(),
                        Text_az = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AlterColumn("dbo.Users", "Token", c => c.String(nullable: false, maxLength: 100));
        }
    }
}
