using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SocialNetwork.Models
{   
    [BsonIgnoreExtraElements]
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string UserName { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public bool InRelationship { get; set; }
        public string UserToFollow { get; set; }
        public string UserToAddToCircle { get; set; }
        public string CircleName { get; set; }
        public List<string> FollowingUserIds { get; set; }
        public List<Circle> Circles { get; set; }
    }
}
