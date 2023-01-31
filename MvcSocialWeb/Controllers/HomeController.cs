﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MvcSocialWeb.ViewModels;
using MvcSocialWeb.ViewModels.Users;
using MvcSocialWeb.ViewModels.Account;
using System.Diagnostics;
using MvcSocialWeb.Data.DBModel.Users;

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
        public IActionResult Index()
        {
            if(User.Identity.IsAuthenticated)
            {
                var taskUser = _userManager.GetUserAsync(User);
                return RedirectToAction("MyPage", "AccountManager");
            }
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