using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MvcSocialWeb.Data.DBModel.Users;
using MvcSocialWeb.Middlewares.Services;
using MvcSocialWeb.ViewModels;
using MvcSocialWeb.ViewModels.Account;
using System.Diagnostics;

namespace MvcSocialWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserGeneration _userGen;
        private readonly UserManager<User> _userManager;

        public HomeController(UserGeneration generation, UserManager<User> userManager)
        {
            _userGen = generation;
            _userManager = userManager;
        }

        /// <summary>
        /// Отображение главной страницы
        /// </summary>
        public IActionResult Index(string returnUrl)
        {
            if(User!.Identity!.IsAuthenticated)
                return RedirectToAction("MyPage", "AccountManager");
            else
                return View(new AccountView(returnUrl));
        }

        /// <summary>
        /// Страница политики безопасности
        /// </summary>
        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// Генерация тестовых пользователей
        /// </summary>
        [HttpGet]
        [Route("Generate/{value}")]
        public async Task<IActionResult> Generate([FromRoute]string value)
        {
            var userList = _userGen.Populate(value);
            if (userList.Count > 0)
            {
                foreach(var user in userList)
                {
                    var result = await _userManager.CreateAsync(user, "11111");

                    if (!result.Succeeded)
                        continue;
                }
            }

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}