using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Albums.Models;
using System.Net.Http;
using Newtonsoft.Json;

namespace Albums.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlbumsController : ControllerBase
    {

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<AlbumsController> _logger;

        public AlbumsController(IHttpClientFactory httpClientFactory, ILogger<AlbumsController> logger){
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        [HttpGet]
        [Route("userCollection/{userId}")]
        public async Task<ActionResult<User>> UsersCollection (int userId)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                client.BaseAddress = new Uri("http://jsonplaceholder.typicode.com");

                var albumResonse = await client.GetStringAsync("albums");
                var allAlbums = JsonConvert.DeserializeObject<List<AlbumModel>>(albumResonse);

                var photoResponse = await client.GetStringAsync("photos");
                var allPhotos = JsonConvert.DeserializeObject<List<PhotoModel>>(photoResponse);

                var userAlbumList = from album in allAlbums
                    join photo in allPhotos on album.albumId equals photo.albumId
                    where album.userId == userId
                    group photo by album.albumId into g
                    select new UserAlbum {
                        albumId = g.Key,
                        userPhotos = g.Select(x=> new UserPhoto{
                            photoId = x.photoId,
                            title = x.title,
                            url = x.url,
                            thumbnailUrl = x.thumbnailUrl
                        }).ToList()
                    };

                if(userAlbumList.Count()==0)
                {
                    _logger.LogTrace($"UsersCollectionAPI:{userId}", "NotFound");
                    return NotFound();
                }
                else
                {
                    User res = new User();
                    res.userId = userId;
                    res.albumList = userAlbumList.ToList();
                    _logger.LogTrace($"UsersCollectionAPI:{userId}", res);
                    return Ok(res);
                }

            }
            catch(Exception ex){
                _logger.LogError(ex, $"UsersCollectionAPI:{userId}-{ex.Message}-{ex.StackTrace}",null);
                return StatusCode(500);
            }

        }
    }
}
