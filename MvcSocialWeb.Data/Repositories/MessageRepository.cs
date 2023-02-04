using Microsoft.EntityFrameworkCore;
using MvcSocialWeb.Data.DBModel.Messages;
using MvcSocialWeb.Data.DBModel.Users;

namespace MvcSocialWeb.Data.Repositories
{
    /// <summary>
    /// Репозиторий сообщений
    /// </summary>
    public class MessageRepository : Repository<Message>
    {
        public MessageRepository(SocialWebContext context) : base(context) { }

        /// <summary>
        /// Получение переписки 2-х пользователей
        /// </summary>
        public async Task<List<Message>> GetMessagesAsync(User sender, User recipient)
        {
            Set.Include(x => x.Recipient).Include(x => x.Sender);

            var from = Set.AsEnumerable()
                .Where(x => x.SenderId == sender.Id && x.RecipientId == recipient.Id);
            var to = Set.AsEnumerable()
                .Where(x => x.SenderId == recipient.Id && x.RecipientId == sender.Id);

            var fromList = await Task.Run(() => from.ToList());
            var toList = await Task.Run(() => to.ToList());

            var total = new List<Message>();
            await Task.Run(() =>
            {
                total.AddRange(fromList);
                total.AddRange(toList);
                total.OrderBy(x => x.Id);
            });

            return total;
        }
    }
}
