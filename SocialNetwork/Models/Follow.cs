using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Models
{
    public class Follow
    {
        [ForeignKey("User")]
        public int RefUserId { get; set; }
    }
}
