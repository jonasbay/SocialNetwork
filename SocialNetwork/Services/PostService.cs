using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using SocialNetwork.Models;

namespace SocialNetwork.Services
{
    public class PostService
    {
        private readonly IMongoCollection<Post> _posts;

        public PostService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("mongodb://localhost:27017"));
            var database = client.GetDatabase("SocialNetworkDb");

            _posts = database.GetCollection<Post>("Posts");
        }

        public List<Post> Get() =>
            _posts.Find(post => true).ToList();

        public Post Create(Post post)
        {
            _posts.InsertOne(post);
            return post;
        }
    }
}
