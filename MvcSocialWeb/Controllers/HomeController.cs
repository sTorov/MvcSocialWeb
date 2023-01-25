using Microsoft.AspNetCore.Mvc;
using MvcSocialWeb.ViewModels;
using MvcSocialWeb.ViewModels.Account;
using System.Diagnostics;

namespace MvcSocialWeb.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// Отображение главной страницы
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            if(User.Identity.IsAuthenticated)
                return View("StartPage");
            else
                return View(new AccountView());
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