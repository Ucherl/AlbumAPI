using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Albums.Models
{
    public class PhotoModel
    {
        public int albumId { get; set; }

        [JsonProperty(PropertyName = "id")]
        public int photoId { get; set; }

        public string title { get; set; }

        public string url { get; set; }

        public string thumbnailUrl { get; set; }
    }

    public class AlbumModel
    {
        [JsonProperty(PropertyName = "id")]
        public int albumId { get; set; }

        public int userId { get; set; }

        public string title { get; set; }
    }

}