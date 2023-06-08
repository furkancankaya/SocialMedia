namespace SocialMedia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firts : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        Title = c.Int(nullable: false),
                        ContentDescription = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.ContentComments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OwnerId = c.Int(nullable: false),
                        ContentId = c.Int(nullable: false),
                        Comment = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        OwnerUser_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contents", t => t.ContentId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.OwnerUser_Id)
                .Index(t => t.ContentId)
                .Index(t => t.OwnerUser_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Surname = c.String(),
                        UserName = c.String(),
                        Mail = c.String(),
                        Password = c.String(),
                        Biography = c.String(),
                        Photo = c.String(),
                        IsStatus = c.Boolean(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.ContentLikes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OwnerId = c.Int(nullable: false),
                        ContentId = c.Int(nullable: false),
                        Like = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        OwnerUser_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contents", t => t.ContentId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.OwnerUser_Id)
                .Index(t => t.ContentId)
                .Index(t => t.OwnerUser_Id);
            
            CreateTable(
                "dbo.Follows",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FollowerUserId = c.Int(nullable: false),
                        FollowedUserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.FollowedUserId)
                .ForeignKey("dbo.Users", t => t.FollowerUserId)
                .Index(t => t.FollowerUserId)
                .Index(t => t.FollowedUserId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Follows", "FollowerUserId", "dbo.Users");
            DropForeignKey("dbo.Follows", "FollowedUserId", "dbo.Users");
            DropForeignKey("dbo.Contents", "UserId", "dbo.Users");
            DropForeignKey("dbo.ContentLikes", "OwnerUser_Id", "dbo.Users");
            DropForeignKey("dbo.ContentLikes", "ContentId", "dbo.Contents");
            DropForeignKey("dbo.ContentComments", "OwnerUser_Id", "dbo.Users");
            DropForeignKey("dbo.ContentComments", "ContentId", "dbo.Contents");
            DropIndex("dbo.Follows", new[] { "FollowedUserId" });
            DropIndex("dbo.Follows", new[] { "FollowerUserId" });
            DropIndex("dbo.ContentLikes", new[] { "OwnerUser_Id" });
            DropIndex("dbo.ContentLikes", new[] { "ContentId" });
            DropIndex("dbo.Users", new[] { "RoleId" });
            DropIndex("dbo.ContentComments", new[] { "OwnerUser_Id" });
            DropIndex("dbo.ContentComments", new[] { "ContentId" });
            DropIndex("dbo.Contents", new[] { "UserId" });
            DropTable("dbo.Roles");
            DropTable("dbo.Follows");
            DropTable("dbo.ContentLikes");
            DropTable("dbo.Users");
            DropTable("dbo.ContentComments");
            DropTable("dbo.Contents");
        }
    }
}
