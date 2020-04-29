using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Models
{
    public class Circle
    {
        public int CircleId { get; set; }
        public string CircleName { get; set; }

        public int UserId { get; set; }
    }
}
