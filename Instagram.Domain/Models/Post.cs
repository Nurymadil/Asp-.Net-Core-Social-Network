using System;
using System.Collections.Generic;

namespace Instagram.Domain
{
    public class Post
    {
        public int Id { get; set; }
        public IList<Like> Likes { get; set; }=new List<Like>();
        public string Photo { get; set; } = "https://picsum.photos/200";
        public string Description { get; set; }
        public string UserId { get; set; }
        public DateTime CreationTime { get; set; } = DateTime.Now;
        public IList<Coment> Coments { get; set; }=new List<Coment>();
    }
}