using Microsoft.AspNetCore.Http;

namespace Instagram.Models.ViewModels
{
    public class CreatePostViewModel
    {
        public string Description { get; set; }
        public IFormFile Photo { get; set; }
    }
}