using Microsoft.EntityFrameworkCore;

namespace MvcSocialWeb.Data
{
    public class SocialWebContext : DbContext
    {
        public SocialWebContext(DbContextOptions<SocialWebContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
