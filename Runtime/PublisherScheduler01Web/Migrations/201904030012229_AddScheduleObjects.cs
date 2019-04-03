namespace PublisherScheduler01Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddScheduleObjects : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Assignment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SlotId = c.Int(nullable: false),
                        TaskTypeId = c.Int(nullable: false),
                        PersonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Person", t => t.PersonId, cascadeDelete: true)
                .ForeignKey("dbo.TaskType", t => t.TaskTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Slot", t => t.SlotId, cascadeDelete: true)
                .Index(t => t.SlotId)
                .Index(t => t.TaskTypeId)
                .Index(t => t.PersonId);
            
            CreateTable(
                "dbo.Capacity",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Person",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PersonAvail",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Begin = c.DateTime(nullable: false),
                        End = c.DateTime(nullable: false),
                        IsAvailable = c.Boolean(nullable: false),
                        Person_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Person", t => t.Person_Id)
                .Index(t => t.Person_Id);
            
            CreateTable(
                "dbo.TaskType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Location",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Slot",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Begin = c.DateTime(nullable: false),
                        End = c.DateTime(nullable: false),
                        LocationId = c.Int(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Location", t => t.LocationId)
                .Index(t => t.LocationId);
            
            CreateTable(
                "dbo.PersonCapacity",
                c => new
                    {
                        Person_Id = c.Int(nullable: false),
                        Capacity_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Person_Id, t.Capacity_Id })
                .ForeignKey("dbo.Person", t => t.Person_Id, cascadeDelete: true)
                .ForeignKey("dbo.Capacity", t => t.Capacity_Id, cascadeDelete: true)
                .Index(t => t.Person_Id)
                .Index(t => t.Capacity_Id);
            
            CreateTable(
                "dbo.TaskTypeCapacity",
                c => new
                    {
                        TaskType_Id = c.Int(nullable: false),
                        Capacity_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TaskType_Id, t.Capacity_Id })
                .ForeignKey("dbo.TaskType", t => t.TaskType_Id, cascadeDelete: true)
                .ForeignKey("dbo.Capacity", t => t.Capacity_Id, cascadeDelete: true)
                .Index(t => t.TaskType_Id)
                .Index(t => t.Capacity_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Slot", "LocationId", "dbo.Location");
            DropForeignKey("dbo.Assignment", "SlotId", "dbo.Slot");
            DropForeignKey("dbo.TaskTypeCapacity", "Capacity_Id", "dbo.Capacity");
            DropForeignKey("dbo.TaskTypeCapacity", "TaskType_Id", "dbo.TaskType");
            DropForeignKey("dbo.Assignment", "TaskTypeId", "dbo.TaskType");
            DropForeignKey("dbo.PersonAvail", "Person_Id", "dbo.Person");
            DropForeignKey("dbo.PersonCapacity", "Capacity_Id", "dbo.Capacity");
            DropForeignKey("dbo.PersonCapacity", "Person_Id", "dbo.Person");
            DropForeignKey("dbo.Assignment", "PersonId", "dbo.Person");
            DropIndex("dbo.TaskTypeCapacity", new[] { "Capacity_Id" });
            DropIndex("dbo.TaskTypeCapacity", new[] { "TaskType_Id" });
            DropIndex("dbo.PersonCapacity", new[] { "Capacity_Id" });
            DropIndex("dbo.PersonCapacity", new[] { "Person_Id" });
            DropIndex("dbo.Slot", new[] { "LocationId" });
            DropIndex("dbo.PersonAvail", new[] { "Person_Id" });
            DropIndex("dbo.Assignment", new[] { "PersonId" });
            DropIndex("dbo.Assignment", new[] { "TaskTypeId" });
            DropIndex("dbo.Assignment", new[] { "SlotId" });
            DropTable("dbo.TaskTypeCapacity");
            DropTable("dbo.PersonCapacity");
            DropTable("dbo.Slot");
            DropTable("dbo.Location");
            DropTable("dbo.TaskType");
            DropTable("dbo.PersonAvail");
            DropTable("dbo.Person");
            DropTable("dbo.Capacity");
            DropTable("dbo.Assignment");
        }
    }
}
