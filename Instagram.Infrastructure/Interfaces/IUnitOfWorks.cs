using Microsoft.AspNetCore.Identity;

namespace Instagram.Domain
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