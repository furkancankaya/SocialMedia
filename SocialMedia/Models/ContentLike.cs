using System;
using System.ComponentModel.DataAnnotations;

namespace SocialMedia.Models
{
    public class ContentLike
    {
        [Key]
        public int Id { get; set; }
        public int? OwnerLikeId { get; set; }
        public User OwnerLike { get; set; }
        public int ContentId { get; set; }
        public Content Content { get; set; }
        public bool Like { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}