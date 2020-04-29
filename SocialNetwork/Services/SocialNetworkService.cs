﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using SocialNetwork.Models;

namespace SocialNetwork.Services
{
    public class SocialNetworkService
    {
        private readonly IMongoCollection<User> _users;

        public SocialNetworkService()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("SocialNetworkDb");

            _users = database.GetCollection<User>("Users");
        }

        public List<User> Get() =>
            _users.Find(user => true).ToList();
    }
}
