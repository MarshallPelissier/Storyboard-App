namespace Storyboard_App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedProjectToPageModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Pages", "Project_Id", "dbo.Projects");
            DropIndex("dbo.Pages", new[] { "Project_Id" });
            RenameColumn(table: "dbo.Pages", name: "Project_Id", newName: "ProjectId");
            AlterColumn("dbo.Pages", "ProjectId", c => c.Int(nullable: false));
            CreateIndex("dbo.Pages", "ProjectId");
            AddForeignKey("dbo.Pages", "ProjectId", "dbo.Projects", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pages", "ProjectId", "dbo.Projects");
            DropIndex("dbo.Pages", new[] { "ProjectId" });
            AlterColumn("dbo.Pages", "ProjectId", c => c.Int());
            RenameColumn(table: "dbo.Pages", name: "ProjectId", newName: "Project_Id");
            CreateIndex("dbo.Pages", "Project_Id");
            AddForeignKey("dbo.Pages", "Project_Id", "dbo.Projects", "Id");
        }
    }
}
