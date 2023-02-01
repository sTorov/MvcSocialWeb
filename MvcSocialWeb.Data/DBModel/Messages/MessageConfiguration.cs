using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MvcSocialWeb.Data.DBModel.Messages
{
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.ToTable("Messages").HasKey(e => e.Id);
            builder.Property(e => e.Id).UseIdentityColumn();
        }
    }
}
