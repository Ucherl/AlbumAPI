using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Albums.Models;

namespace Albums.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlbumsController : ControllerBase
    {

        [HttpGet]
        [Route("userCollection/{userId}")]
        public ActionResult<List<User>> UsersCollection (int userId)
        {
            return Ok();
        }
    }
}
