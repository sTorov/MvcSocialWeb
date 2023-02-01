using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MvcSocialWeb.Data.DBModel.Messages;
using MvcSocialWeb.Data.DBModel.Users;
using MvcSocialWeb.Data.Repositories;
using MvcSocialWeb.Middlewares.Services;
using MvcSocialWeb.ViewModels.Messages;

namespace MvcSocialWeb.Controllers
{
    /// <summary>
    /// Контроллер сообщений
    /// </summary>
    public class MessageController : Controller
    {
        private readonly UserServices _userServices;

        public MessageController(UserServices userServices)
        {
            _userServices = userServices;
        }

        /// <summary>
        /// Отображение страницы переписки с пользователем
        /// </summary>
        [Route("Chat")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Chat(string id)
        {
            var items = await _userServices.GetItemForManipulation<Message, MessageRepository>(User, id);

            var model = GetCharModelView(items.user!, items.friend!, items.repo!);

            return View(model);
        }

        /// <summary>
        /// Отправка нового сообщения в чат
        /// </summary>
        [Route("NewMessage")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> NewMessage(string id, ChatViewModel chat)
        {
            var items = await _userServices.GetItemForManipulation<Message, MessageRepository>(User, id);

            var newMessage = new Message()
            {
                Sender = items.user!,
                Recipient = items.friend!,
                Text = chat.NewMessage.Text
            };

            await items.repo?.CreateAsync(newMessage)!;

            var model = GetCharModelView(items.user!, items.friend!, items.repo!);
            
            return View(model);
        }

        /// <summary>
        /// Получение модели данных чата
        /// </summary>
        private async Task<ChatViewModel> GetCharModelView(User user, User friend, MessageRepository repo)
        {
            var history = await repo.GetMessages(user, friend);

            return new ChatViewModel()
            {
                User = user,
                Friend = friend,
                History = history
            };
        }
    }
}
