﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Models
{
    public class Circle
    {
        public string Name { get; set; }
        public List<string> UserIds { get; set; }
    }
}
