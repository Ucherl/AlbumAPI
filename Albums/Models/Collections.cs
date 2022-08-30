using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Albums.Models
{
    public class Photo
    {
        public int albumId { get; set; }

        [JsonProperty(PropertyName = "id")]
        public int photoId { get; set; }

        public string title { get; set; }

        public string url { get; set; }

        public string thumbnailUrl { get; set; }
    }

    public class Album
    {
        [JsonProperty(PropertyName = "id")]
        public int albumId { get; set; }

        public int userId { get; set; }

        public string title { get; set; }
    }

    public class User 
    {
        public int userId { get; set; }

        public List<Album> albumList { get; set; }
    }
}