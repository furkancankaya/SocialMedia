using SocialMedia.Models;
using System.Collections.Generic;

namespace SocialMedia.Models
{
    public class ContentViewModel
    {
        public Content Content { get; set; }
        public List<ContentLike> Likes{ get; set; }

        public List<ContentComment> ContentComment { get; set; }
    }
}