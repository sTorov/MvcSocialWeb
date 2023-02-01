using MvcSocialWeb.Data.DBModel.Messages;

namespace MvcSocialWeb.Data.Repositories
{
    public class MessageRepository : Repository<Message>
    {
        public MessageRepository(SocialWebContext context) : base(context) { }
    }
}
