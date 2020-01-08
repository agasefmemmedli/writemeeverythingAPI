namespace writeMeEverythingASP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddErrorMessages : DbMigration
    {
        public override void Up()
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ErrorMessages");
        }
    }
}
