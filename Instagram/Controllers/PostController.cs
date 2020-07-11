using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Instagram.Models;
using Instagram.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Instagram.Domain;
using Microsoft.AspNetCore.Hosting;

namespace Instagram.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private readonly IUnitOfWorks _uow;
        private readonly IMapper _mapper;
        private string UserId { get; set; }
        private readonly IWebHostEnvironment _environment;

        public PostController(IUnitOfWorks uow, IMapper mapper, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment environment)
        {
            _uow = uow;
            _mapper = mapper;
            _environment = environment;
            UserId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

        }

        // GET: Post
        public IActionResult Index()
        {
            var posts = _uow.Posts.GetAll().OrderByDescending(x => x.CreationTime).ToList();
            var postsViewModel = _mapper.Map<IList<PostViewModel>>(posts);
            return View(postsViewModel);
        }


        public IActionResult Create()
        {
            return View();
        }

        // POST: Post/Create
        [HttpPost]
        public IActionResult Create(CreatePostViewModel postViewModel)
        {
            _uow.BeginTransaction();
            try
            {
                if (!ModelState.IsValid) return View();
                var post = _mapper.Map<Post>(postViewModel);
                post.UserId = UserId;
                if (postViewModel.Photo != null)
                {
                    var path = "/Files" + postViewModel.Photo.FileName;
                    using var fileStream = new FileStream(_environment.WebRootPath + path, FileMode.Create);
                    postViewModel.Photo.CopyTo(fileStream);

                    post.Photo = path;
                }
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
        public JsonResult Like(string id)
        {
            int.TryParse(id, out var postId);
            var post = _uow.Posts.Get(postId);
            var like = post.Likes.FirstOrDefault(x => x.Post.Id == post.Id);
            var likeExist = like != null;
            if (!likeExist)
            {
                like = new Like { Post = post, UserId = post.UserId };
                post.Likes.Add(like);
            }
            else post.Likes.Remove(like);

            _uow.Posts.Edit(post);
            _uow.Save();
            return Json(new { post.Likes.Count });

        }

        [HttpPost]
        public JsonResult AddComent(string id, string text)
        {
            int.TryParse(id, out var postId);
            var post = _uow.Posts.Get(postId);
            var coment = new Coment
            {
                Post = post,
                Text = text,
                UserId = post.UserId
            };
            post.Coments.Add(coment);
            _uow.Posts.Edit(post);
            _uow.Save();
            return Json(true);
        }
    }
}
