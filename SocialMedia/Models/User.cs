using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SocialMedia.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public string Biography { get; set; }
        public string Photo { get; set; }
        public bool IsStatus { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public List<Content> Contents { get; set; }
        public List<ContentComment> ContentComments { get; set; }
        public List<Follow> FollowerUser { get; set; }
        public List<Follow> FollowedUser { get; set; }
        public List<ContentLike> ContentLikes { get; set; }

    }
}
