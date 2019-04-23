namespace PublisherScheduler01Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingAttributes : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Slot", "LocationId", "dbo.Location");
            DropIndex("dbo.Slot", new[] { "LocationId" });
            AlterColumn("dbo.Capacity", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Person", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.TaskType", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Location", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Slot", "LocationId", c => c.Int(nullable: false));
            CreateIndex("dbo.Slot", "LocationId");
            AddForeignKey("dbo.Slot", "LocationId", "dbo.Location", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Slot", "LocationId", "dbo.Location");
            DropIndex("dbo.Slot", new[] { "LocationId" });
            AlterColumn("dbo.Slot", "LocationId", c => c.Int());
            AlterColumn("dbo.Location", "Name", c => c.String());
            AlterColumn("dbo.TaskType", "Name", c => c.String());
            AlterColumn("dbo.Person", "Name", c => c.String());
            AlterColumn("dbo.Capacity", "Name", c => c.String());
            CreateIndex("dbo.Slot", "LocationId");
            AddForeignKey("dbo.Slot", "LocationId", "dbo.Location", "Id");
        }
    }
}
