using System.ComponentModel.DataAnnotations;

namespace SocialMedia.Models
{
    public class Follow
    {
        [Key]
        public int Id { get; set; }
        public int FollowerUserId { get; set; }
        public User FollowerUser { get; set; }
        public int FollowedUserId { get; set; }
        public User FollowedUser { get; set; }

    }
}
