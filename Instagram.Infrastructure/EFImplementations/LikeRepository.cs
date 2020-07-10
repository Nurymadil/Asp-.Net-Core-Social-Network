using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.EntityFramework;
using Instagram.Domain;
using Microsoft.EntityFrameworkCore;

namespace Instagram.Infrastructure.EFImplementations
{
    public class LikeRepository:IRepository<Like>
    {
        private readonly ApplicationDbContext _context;

        public LikeRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public Like Get(int id)
        {
            return _context.Likes.Find(id);
        }
        public IList<Like> GetAll()
        {
            return _context.Likes.ToList();
        }
        public void Create(Like entity)
        {
            _context.Likes.Add(entity);
        }
        public Like Edit(Like entity)
        {
            try
            {
                _context.Entry(entity).State = EntityState.Modified;
                return _context.Likes.First(x => x.Id == entity.Id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void Delete(Like entity)
        {
            _context.Likes.Remove(entity);
        }

    }
}