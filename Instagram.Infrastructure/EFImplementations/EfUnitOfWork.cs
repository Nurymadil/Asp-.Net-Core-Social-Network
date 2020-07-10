using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.EntityFramework;
using Instagram.Domain;
using Microsoft.AspNetCore.Identity;

namespace Instagram.Infrastructure.EFImplementations
{
  public  class EfUnitOfWork : IUnitOfWorks
    {
        private readonly ApplicationDbContext _context;
        //ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
        public IRepository<Post> Posts { get; set; }
        public IRepository<IdentityUser> Users { get; set; }

        public EfUnitOfWork(IRepository<Post> posts,IRepository<IdentityUser> users, ApplicationDbContext context)
        {
            Posts = posts;
            Users = users;
            _context = context;
        }
        public void Save()
        {
         var saved=   _context.SaveChanges();
        }

        public void BeginTransaction()
        {
            _context.Database.BeginTransaction();
        }

        public void Commit()
        {
            _context.Database.CurrentTransaction?.Commit();
        }

        public void Rollback()
        {
            _context.Database.CurrentTransaction?.Rollback();
        }
    }
}
