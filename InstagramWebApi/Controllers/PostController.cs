using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Instagram.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InstagramWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IUnitOfWorks _uow;
        private readonly IMapper _mapper;

        public PostController(IUnitOfWorks uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IList<Post>> Get()
        {
            var posts = _uow.Posts.GetAll();
            if (posts == null) return NotFound();
            return posts.ToList();
        }
        [HttpGet("{postId}")]
        public ActionResult<Post> Get(int postId)
        {
            var post = _uow.Posts.Get(postId);
            if (post == null) return NotFound();
            return post;
        }

        [HttpPost]
        public ActionResult<Post> Create(Post post)
        {
            if (post == null) return BadRequest();
            _uow.Posts.Create(post);
            _uow.Save();
            return Ok(post);
        }

    }
}
