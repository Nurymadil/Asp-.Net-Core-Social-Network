using System;
using System.Collections.Generic;
using System.Linq;
using Instagram.Data;
using Instagram.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Instagram.Infrastructure.EFImplementations
{
    public class UserRepository:IRepository<IdentityUser>
    { private readonly ApplicationDbContext _context;

        public IdentityUser Get(int id)
        {
            return _context.Users.Find(id);
        }
        public IList<IdentityUser> GetAll()
        {
            return _context.Users.ToList();
        }

        public void Create(IdentityUser entity)
        {
            throw new System.NotImplementedException();
        }

        public IdentityUser Edit(IdentityUser entity)
        {
            try
            {
                _context.Entry(entity).State = EntityState.Modified;
                return _context.Users.Find(entity);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void Delete(IdentityUser entity)
        {
            _context.Users.Remove(entity);
        }
    }
}