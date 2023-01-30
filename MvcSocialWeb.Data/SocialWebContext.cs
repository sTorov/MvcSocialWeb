using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MvcSocialWeb.Data.DBModel;
using MvcSocialWeb.Data.DBModel.Friend;

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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new FriendConfiguration());
        }
    }
}
