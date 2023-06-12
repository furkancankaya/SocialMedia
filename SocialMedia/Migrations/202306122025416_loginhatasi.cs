namespace SocialMedia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class loginhatasi : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contents", "ObjectPath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Contents", "ObjectPath");
        }
    }
}
