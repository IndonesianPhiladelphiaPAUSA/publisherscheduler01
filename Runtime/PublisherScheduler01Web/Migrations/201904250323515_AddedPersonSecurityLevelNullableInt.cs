namespace PublisherScheduler01Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPersonSecurityLevelNullableInt : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Person", "SecurityLevel", c => c.Int());
            AlterColumn("dbo.AspNetUsers", "PublisherName", c => c.String());
            AlterColumn("dbo.AspNetUsers", "CongregationNo", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "CongregationNo", c => c.String(nullable: false));
            AlterColumn("dbo.AspNetUsers", "PublisherName", c => c.String(nullable: false));
            DropColumn("dbo.Person", "SecurityLevel");
        }
    }
}
