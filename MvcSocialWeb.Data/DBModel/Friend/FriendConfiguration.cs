using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MvcSocialWeb.Data.DBModel.Friend
{
    public class FriendConfiguration : IEntityTypeConfiguration<Friend>
    {
        public void Configure(EntityTypeBuilder<Friend> builder)
        {
            builder.ToTable("UserFriends").HasKey(t => t.Id);
            builder.Property(p => p.Id).UseIdentityColumn();
        }
    }
}
