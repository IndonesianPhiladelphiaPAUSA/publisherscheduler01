namespace PublisherScheduler01Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSchedulerObjects : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Assignment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
                "dbo.SlotFill",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SlotId = c.Int(nullable: false),
                        AssignmentId = c.Int(nullable: false),
                        PersonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Person", t => t.PersonId, cascadeDelete: true)
                .ForeignKey("dbo.Assignment", t => t.AssignmentId, cascadeDelete: true)
                .ForeignKey("dbo.Slot", t => t.SlotId, cascadeDelete: true)
                .Index(t => t.SlotId)
                .Index(t => t.AssignmentId)
                .Index(t => t.PersonId);
            
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
                "dbo.CapacityAssignment",
                c => new
                    {
                        Capacity_Id = c.Int(nullable: false),
                        Assignment_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Capacity_Id, t.Assignment_Id })
                .ForeignKey("dbo.Capacity", t => t.Capacity_Id, cascadeDelete: true)
                .ForeignKey("dbo.Assignment", t => t.Assignment_Id, cascadeDelete: true)
                .Index(t => t.Capacity_Id)
                .Index(t => t.Assignment_Id);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Slot", "LocationId", "dbo.Location");
            DropForeignKey("dbo.SlotFill", "SlotId", "dbo.Slot");
            DropForeignKey("dbo.SlotFill", "AssignmentId", "dbo.Assignment");
            DropForeignKey("dbo.SlotFill", "PersonId", "dbo.Person");
            DropForeignKey("dbo.PersonAvail", "Person_Id", "dbo.Person");
            DropForeignKey("dbo.PersonCapacity", "Capacity_Id", "dbo.Capacity");
            DropForeignKey("dbo.PersonCapacity", "Person_Id", "dbo.Person");
            DropForeignKey("dbo.CapacityAssignment", "Assignment_Id", "dbo.Assignment");
            DropForeignKey("dbo.CapacityAssignment", "Capacity_Id", "dbo.Capacity");
            DropIndex("dbo.PersonCapacity", new[] { "Capacity_Id" });
            DropIndex("dbo.PersonCapacity", new[] { "Person_Id" });
            DropIndex("dbo.CapacityAssignment", new[] { "Assignment_Id" });
            DropIndex("dbo.CapacityAssignment", new[] { "Capacity_Id" });
            DropIndex("dbo.Slot", new[] { "LocationId" });
            DropIndex("dbo.SlotFill", new[] { "PersonId" });
            DropIndex("dbo.SlotFill", new[] { "AssignmentId" });
            DropIndex("dbo.SlotFill", new[] { "SlotId" });
            DropIndex("dbo.PersonAvail", new[] { "Person_Id" });
            DropTable("dbo.PersonCapacity");
            DropTable("dbo.CapacityAssignment");
            DropTable("dbo.Slot");
            DropTable("dbo.Location");
            DropTable("dbo.SlotFill");
            DropTable("dbo.PersonAvail");
            DropTable("dbo.Person");
            DropTable("dbo.Capacity");
            DropTable("dbo.Assignment");
        }
    }
}
