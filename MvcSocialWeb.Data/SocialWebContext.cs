using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MvcSocialWeb.Data.DBModel;

namespace MvcSocialWeb.Data
{
    /// <summary>
    /// Класс контекста, предоставляющий доступ к сущности базы данных
    /// </summary>
    public class SocialWebContext : IdentityDbContext<User>
    {
        public SocialWebContext(DbContextOptions<SocialWebContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
