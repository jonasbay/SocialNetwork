using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Models
{
    public class SocialNetworkViewModel
    {
        public List<User> users { get; set; }
        public List<Post> posts { get; set; }
        public List<Comment> comments { get; set; }
        public List<Circle> circles { get; set; }
    }
}
