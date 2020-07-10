namespace Instagram.Domain
{
    public class Coment
    {
        public int Id { get; set; }
        public Post Post { get; set; }
        public string UserId { get; set; }
        public string Text { get; set; }
    }
}