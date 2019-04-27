namespace PublisherScheduler01Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PersonAvailRefactoringPersonId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PersonAvail", "Person_Id", "dbo.Person");
            DropIndex("dbo.PersonAvail", new[] { "Person_Id" });
            RenameColumn(table: "dbo.PersonAvail", name: "Person_Id", newName: "PersonId");
            AlterColumn("dbo.PersonAvail", "PersonId", c => c.Int(nullable: false));
            CreateIndex("dbo.PersonAvail", "PersonId");
            AddForeignKey("dbo.PersonAvail", "PersonId", "dbo.Person", "Id", cascadeDelete: true);
            DropColumn("dbo.PersonAvail", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PersonAvail", "UserId", c => c.Int(nullable: false));
            DropForeignKey("dbo.PersonAvail", "PersonId", "dbo.Person");
            DropIndex("dbo.PersonAvail", new[] { "PersonId" });
            AlterColumn("dbo.PersonAvail", "PersonId", c => c.Int());
            RenameColumn(table: "dbo.PersonAvail", name: "PersonId", newName: "Person_Id");
            CreateIndex("dbo.PersonAvail", "Person_Id");
            AddForeignKey("dbo.PersonAvail", "Person_Id", "dbo.Person", "Id");
        }
    }
}
