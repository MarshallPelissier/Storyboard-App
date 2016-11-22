namespace Storyboard_App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PageNameToNum : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pages", "Num", c => c.Int(nullable: false));
            DropColumn("dbo.Pages", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Pages", "Name", c => c.String());
            DropColumn("dbo.Pages", "Num");
        }
    }
}
