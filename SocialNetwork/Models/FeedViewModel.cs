using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Models
{
    public class FeedViewModel
    {
        public List<Post> posts { get; set; }
        public User user { get; set; }
    }
}
