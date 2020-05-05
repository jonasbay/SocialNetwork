using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Models
{
    public class UserViewModel
    {
        public string UserId { get; set; }
        public List<Circle> Circles { get; set; }
    }
}
