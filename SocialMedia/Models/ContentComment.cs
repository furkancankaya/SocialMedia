using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMedia.Models
{
    public class ContentComment
    {
        [Key]
        public int Id { get; set; }
        public int? OwnerId { get; set; }
        public User Owner { get; set; }
        public int ContentId { get; set; }
        public Content Content { get; set; }
        public string Comment { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;

        [NotMapped]
        public string CreateDateHumanReadable { get; set; }
    }

}