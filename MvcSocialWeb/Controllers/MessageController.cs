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
        private readonly ControllerServices _сontrollerServices;

        public MessageController(ControllerServices сontrollerServices)
        {
            _сontrollerServices = сontrollerServices;
        }

        /// <summary>
        /// Отображение страницы переписки с пользователем
        /// </summary>
        [Route("Chat/{id}")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Chat([FromRoute]string id)
        {
            var (user, friend, repo) = await _сontrollerServices.GetItemForManipulation<Message, MessageRepository>(User, id);
            var model = await GetCharModelView(user!, friend!, repo!);

            return View(model);
        }

        /// <summary>
        /// Отправка нового сообщения в чат
        /// </summary>
        [Route("NewMessage")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> NewMessage(string id, ChatViewModel model)
        {
            var (user, friend, repo) = await _сontrollerServices.GetItemForManipulation<Message, MessageRepository>(User, id);

            var newMessage = new Message()
            {
                Sender = user!,
                Recipient = friend!,
                Text = model.NewMessage.Text
            };

            await repo!.CreateAsync(newMessage);

            return Redirect($"/Chat/{id}");
        }

        /// <summary>
        /// Получение модели данных чата
        /// </summary>
        private async Task<ChatViewModel> GetCharModelView(User user, User friend, MessageRepository repo)
        {
            var history = await repo.GetMessagesAsync(user, friend);

            var model = new ChatViewModel()
            {
                User = user,
                Friend = friend,
                History = history.OrderBy(x => x.Id).ToList(),
            };

            return model;
        }
    }
}
