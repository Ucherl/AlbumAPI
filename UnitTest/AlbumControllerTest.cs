using NUnit.Framework;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Albums;
using Albums.Models;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace UnitTest
{
    public class Tests
    {
        private static HttpClient client;
        
        [SetUp]
        public void Setup()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            client = webAppFactory.CreateDefaultClient();
        }

        //user id range : 1~10
        [Test]
        public async Task NoUser()
        {
            var response = await client.GetAsync("api/Albums/userCollection/200");
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test]
        public async Task ExistUser()
        {
            var response = await client.GetAsync("api/Albums/userCollection/1");
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestCase(1, ExpectedResult=10)]
        [TestCase(3, ExpectedResult=10)]
        [TestCase(10, ExpectedResult=10)]
        public async Task<int> ValidationaAlbumCount(int userId){
            var response = await client.GetStringAsync($"api/Albums/userCollection/{userId}");
            var user = JsonConvert.DeserializeObject<User>(response);
            return user.albumList.Count;
        }

        [TestCase(2, ExpectedResult=500)]
        [TestCase(5, ExpectedResult=500)]
        public async Task<int> ValidationaPhotoCount(int userId){
            var response = await client.GetStringAsync($"api/Albums/userCollection/{userId}");
            var user = JsonConvert.DeserializeObject<User>(response);
            return user.albumList.Sum(x=> x.userPhotos.Count);
        }
    }
}
