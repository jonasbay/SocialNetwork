using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using SocialNetwork.Models;

namespace SocialNetwork.Services
{
    public class CommentService
    {
        private readonly IMongoCollection<Comment> _comment;

        public CommentService()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("SocialNetworkDb");

            _comment = database.GetCollection<Comment>("Comments");
        }

        public List<Comment> Get() =>
            _comment.Find(comment => true).ToList();

        public Comment Get(string id) =>
            _comment.Find<Comment>(comment => comment.Id == id).FirstOrDefault();

        public Comment Create(Comment comment)
        {
            _comment.InsertOne(comment);
            return comment;
        }
    }
}
