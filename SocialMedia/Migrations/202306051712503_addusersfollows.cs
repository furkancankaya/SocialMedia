namespace SocialMedia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addusersfollows : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Contents", "Type", c => c.String());
            AlterColumn("dbo.Contents", "Title", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Contents", "Title", c => c.Int(nullable: false));
            AlterColumn("dbo.Contents", "Type", c => c.Int(nullable: false));
        }
    }
}
