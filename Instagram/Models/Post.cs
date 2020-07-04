using System;
using Microsoft.AspNetCore.Identity;

namespace Instagram.Models
{
    public class Post
    {
        public int Id { get; set; }
        public int Likes { get; set; }
        public string Photo { get; set; } = "https://picsum.photos/200";
        public string Description { get; set; }
        public string UserId { get; set; }
        public DateTime CreationTime { get; set; }= DateTime.Now;

    }
}