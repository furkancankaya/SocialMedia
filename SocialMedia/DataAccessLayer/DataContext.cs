using SocialMedia.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StockManagementProject.DataAccessLayer
{
    internal class DataContext : DbContext
    {
        public DataContext() : base("dbConnection") { }


        public DbSet<Role> Role { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Follow> Follow { get; set; }
        public DbSet<ContentComment> ContentComment { get; set; }
        public DbSet<ContentLike> ContentLike { get; set; }
        public DbSet<Content> Content { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Follow>()
                        .HasRequired(m => m.FollowerUser)
                        .WithMany(t => t.FollowedUser)
                        .HasForeignKey(m => m.FollowerUserId)
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<Follow>()
                        .HasRequired(m => m.FollowedUser)
                        .WithMany(t => t.FollowerUser)
                        .HasForeignKey(m => m.FollowedUserId)
                        .WillCascadeOnDelete(false);
        }

    }
}