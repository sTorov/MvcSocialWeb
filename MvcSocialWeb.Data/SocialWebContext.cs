using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MvcSocialWeb.Data.DBModel.Friend;
using MvcSocialWeb.Data.DBModel.Messages;
using MvcSocialWeb.Data.DBModel.Users;

namespace MvcSocialWeb.Data
{
    /// <summary>
    /// Класс контекста, предоставляющий доступ к сущности базы данных
    /// </summary>
    public class SocialWebContext : IdentityDbContext<User>
    {
        public SocialWebContext(DbContextOptions<SocialWebContext> options) : base(options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new FriendConfiguration());
            builder.ApplyConfiguration(new MessageConfiguration());
        }
    }
}
