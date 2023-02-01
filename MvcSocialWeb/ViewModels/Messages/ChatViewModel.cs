using MvcSocialWeb.Data.DBModel.Messages;
using MvcSocialWeb.Data.DBModel.Users;

namespace MvcSocialWeb.ViewModels.Messages
{
    /// <summary>
    /// Модель данных чата
    /// </summary>
    public class ChatViewModel
    {
        public User Friend{ get; set; }
        public User User { get; set; }
        public List<Message> History { get; set; }
        public MessageViewModel NewMessage { get; set; }

        public ChatViewModel() 
        {
            NewMessage = new MessageViewModel();
        }
    }
}
