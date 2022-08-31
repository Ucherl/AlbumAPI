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
        [TestCase(11)]
        [TestCase(0)]
        public async Task NoUser(int userId)
        {
            var response = await client.GetAsync($"api/Albums/userCollection/{userId}");
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestCase(4)]
        [TestCase(9)]
        public async Task ExistUser(int userId)
        {
            var response = await client.GetAsync($"api/Albums/userCollection/{userId}");
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestCase("aab")]
        [TestCase(":)")]
        public async Task InvalidUserInput(string invalidUserId)
        {
            var response = await client.GetAsync($"api/Albums/userCollection/{invalidUserId}");
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
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
