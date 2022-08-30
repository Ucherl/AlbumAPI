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

        public AlbumsController(IHttpClientFactory httpClientFactory){
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        [Route("userCollection/{userId}")]
        public async Task<ActionResult<User>> UsersCollection (int userId)
        {
               
            /**
            case 1 : no resource
            case 2 : there is photos 
            case 3 : bugs
            **/
            try
            {
                var client = _httpClientFactory.CreateClient();
                client.BaseAddress = new Uri("http://jsonplaceholder.typicode.com");

                var albumString = await client.GetStringAsync("albums");
                var allAlbums = JsonConvert.DeserializeObject<List<AlbumModel>>(albumString);

                var photoString = await client.GetStringAsync("photos");
                var allPhotos = JsonConvert.DeserializeObject<List<PhotoModel>>(photoString);

                //get the userId's collection
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

                if(userAlbumList.Count()==0) return NotFound();
                else
                {
                    User res = new User();
                    res.userId = userId;
                    res.albumList = userAlbumList.ToList();
                    return Ok(res);
                }

            }
            catch(Exception ex){
                //log something
                return StatusCode(500);
            }

        }
    }
}
