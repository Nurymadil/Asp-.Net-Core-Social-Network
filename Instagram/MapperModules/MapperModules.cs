using AutoMapper;
using Instagram.Models;
using Instagram.Models.ViewModels;

namespace Instagram.MapperModules
{
    
    public class MapperModules:Profile
    {
        public MapperModules()
        {
            CreateMap<Post, PostViewModel>();
            CreateMap<PostViewModel, Post>();
        }
    }
}