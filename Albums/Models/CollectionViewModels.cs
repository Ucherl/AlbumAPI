using System;
using System.Collections.Generic;

namespace Albums.Models
{
    public class User
    {
        public int userId { get; set; }

        public List<UserAlbum> albumList { get; set; }
    }

    public class UserAlbum
    {
        public int albumId {get;set;}

        public List<UserPhoto> userPhotos {get;set;}
    }

    public class UserPhoto    
    {
        public int photoId { get; set; }

        public string title { get; set; }

        public string url { get; set; }
        public string thumbnailUrl { get; set; }
    }
}