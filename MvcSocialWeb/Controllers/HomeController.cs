using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MvcSocialWeb.Data.DBModel;
using MvcSocialWeb.ViewModels;
using MvcSocialWeb.ViewModels.Users;
using MvcSocialWeb.ViewModels.Account;
using System.Diagnostics;

namespace MvcSocialWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<User> _userManager;

        public HomeController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// Отображение главной страницы
        /// </summary>
        public IActionResult Index(AccountView model)
        {
            if(User.Identity.IsAuthenticated)
            {
                var taskUser = _userManager.GetUserAsync(User);
                return RedirectToAction("MyPage", "AccountManager", new UserViewModel(taskUser.Result));
            }
            else
                return View(model);
        }

        /// <summary>
        /// Страница политики безопасности
        /// </summary>
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}