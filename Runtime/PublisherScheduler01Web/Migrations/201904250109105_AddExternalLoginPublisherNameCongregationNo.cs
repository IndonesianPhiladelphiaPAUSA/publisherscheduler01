namespace PublisherScheduler01Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddExternalLoginPublisherNameCongregationNo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "PublisherName", c => c.String(nullable: false));
            AddColumn("dbo.AspNetUsers", "CongregationNo", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "CongregationNo");
            DropColumn("dbo.AspNetUsers", "PublisherName");
        }
    }
}
