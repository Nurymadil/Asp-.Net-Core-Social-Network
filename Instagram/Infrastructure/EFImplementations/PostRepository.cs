using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Instagram.Data;
using Instagram.Interfaces;
using Instagram.Models;
using Microsoft.EntityFrameworkCore;

namespace Instagram.Infrastructure.EFImplementations
{
    public class PostRepository:IRepository<Post>
    {
        private readonly ApplicationDbContext _context;

        public PostRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public Post Get(int id)
        {
            return _context.Posts.Find(id);
        }
        public  IList<Post> GetAll()
        {
            return _context.Posts.ToList();
        }
        public void Create(Post entity)
        {
            _context.Posts.Add(entity);
        }
        public Post Edit(Post entity)
        {
            try
            {
                _context.Entry(entity).State = EntityState.Modified;
                return _context.Posts.First(x => x.Id == entity.Id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void Delete(Post entity)
        {
            _context.Posts.Remove(entity);
        }

    }
}