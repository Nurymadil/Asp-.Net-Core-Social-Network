using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.EntityFramework;
using Instagram.Domain;
using Microsoft.EntityFrameworkCore;

namespace Instagram.Infrastructure.EFImplementations
{
    public class ComentRepository:IRepository<Coment>
    {
        private readonly ApplicationDbContext _context;

        public ComentRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public Coment Get(int id)
        {
            return _context.Coments.Find(id);
        }
        public IList<Coment> GetAll()
        {
            return _context.Coments.ToList();
        }
        public void Create(Coment entity)
        {
            _context.Coments.Add(entity);
        }
        public Coment Edit(Coment entity)
        {
            try
            {
                _context.Entry(entity).State = EntityState.Modified;
                return _context.Coments.First(x => x.Id == entity.Id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void Delete(Coment entity)
        {
            _context.Coments.Remove(entity);
        }

    }
}