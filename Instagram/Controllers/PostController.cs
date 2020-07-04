using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Instagram.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Instagram.Models;
using Instagram.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Instagram.Controllers
{      [Authorize]
    public class PostController : Controller
    {
        private readonly IUnitOfWorks _uow;
        private readonly IMapper _mapper;
        private string UserId { get; set; }
        public PostController(IUnitOfWorks uow, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _uow = uow;
            _mapper = mapper;
            UserId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

        }

        // GET: Post
        public IActionResult Index()
        {
            var posts = _uow.Posts.GetAll().OrderByDescending(x => x.CreationTime);
            var postsViewModel = _mapper.Map<IList<PostViewModel>>(posts);
            return View(postsViewModel);
        }
        
 
        public IActionResult Create()
        {
            return View();
        }

        // POST: Post/Create
        [HttpPost]
        public IActionResult Create(PostViewModel postViewModel)
        {
            _uow.BeginTransaction();
            try
            {
                if (!ModelState.IsValid) return View();
                var post = _mapper.Map<Post>(postViewModel);
                post.UserId = UserId;
                _uow.Posts.Create(post);
               _uow.Save();
                _uow.Commit();
                return RedirectToAction("Index");
            }
            catch 
            {
                _uow.Rollback();
                return View();
            }
        }

        [HttpPost]
        public IActionResult Like(string id)
        {
            var postId = int.Parse(id);
            var post = _uow.Posts.Get(postId);
            post.Likes++;
            _uow.Posts.Edit(post);
            _uow.Save();
            return Ok();
        }

     
    }
}
