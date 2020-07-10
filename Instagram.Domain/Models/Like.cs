namespace Instagram.Domain
{
    public class Like
    {
        public int Id { get; set; }
        public Post Post { get; set; }
        public string UserId { get; set; }
    }
}