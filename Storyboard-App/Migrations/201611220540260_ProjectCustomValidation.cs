namespace Storyboard_App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProjectCustomValidation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Projects", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Projects", "Name", c => c.String());
        }
    }
}
