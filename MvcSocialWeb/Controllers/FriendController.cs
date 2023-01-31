using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MvcSocialWeb.Data.DBModel.Users;
using MvcSocialWeb.ViewModels.Friend;

namespace MvcSocialWeb.Controllers
{
    public class FriendController : Controller
    {
        private readonly UserManager<User> _userManager;

        public FriendController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// Поиск пользователей
        /// </summary>
        [HttpPost]
        [Route("Search")]
        public IActionResult UserList(string search = "")
        {
            var model = new SearchViewModel()
            {
                FindUsers = _userManager.Users
                .AsEnumerable()
                .Where(u => u.GetFullName().ToLower().Contains(search.ToLower()))
                .ToList()
            };

            return View(model);
        }
    }
}
