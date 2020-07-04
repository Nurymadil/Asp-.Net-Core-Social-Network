using Instagram.Models;
using Microsoft.AspNetCore.Identity;

namespace Instagram.Interfaces
{
    public interface IUnitOfWorks
    {
        IRepository<Post> Posts { get; set; }
        IRepository<IdentityUser> Users { get; set; }
        void Save();

        void BeginTransaction();

        void Commit();

        void Rollback();
    }
}