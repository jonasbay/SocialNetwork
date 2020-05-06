using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using SocialNetwork.Models;

namespace SocialNetwork.Services
{
    public class PostService
    {
        private readonly IMongoCollection<Post> _posts;

        public PostService()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("SocialNetworkDb");

            _posts = database.GetCollection<Post>("Posts");
        }

        public List<Post> Get() =>
            _posts.Find(post => true).ToList();

        public List<Post> GetFeed(string userId) =>
            _posts.Find(post => post.Id == userId).ToList();

        public Post Get(string id) =>
            _posts.Find<Post>(post => post.Id == id).FirstOrDefault();

        public Post Create(Post post)
        {
            _posts.InsertOne(post);
            return post;
        }

        public void Update(string id, Post postIn) =>
            _posts.ReplaceOne(post => post.Id == id, postIn);

        public void UpdateLike(string id, Post postIn)
        {
            //var myPost = postIn;
            //myPost.Likes++;

            //myPost = Update<Post>.Set(p => p.Likes, "1");


            //var filter = Builders<BsonDocument>.Filter.Eq("Id", id);
            //var update = Builders<BsonDocument>.Update.Set("Likes", 1);
            //_posts.UpdateOne();
        }

        public void Remove(Post postIn) =>
            _posts.DeleteOne(post => post.Id == postIn.Id);

        public void Remove(string id) =>
            _posts.DeleteOne(post => post.Id == id);
    }
}
