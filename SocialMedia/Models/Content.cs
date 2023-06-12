using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SocialMedia.Models
{
    public class Content
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public string ObjectPath { get; set; }
        public string ContentDescription { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public List<ContentComment> ContentComments { get; set; }
        public List<ContentLike> ContentLikes { get; set; }
    }
  
}