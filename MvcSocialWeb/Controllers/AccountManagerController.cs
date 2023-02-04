using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MvcSocialWeb.ViewModels.Users;
using MvcSocialWeb.ViewModels.Account;
using MvcSocialWeb.Middlewares.Extensions;
using MvcSocialWeb.Data.DBModel.Users;
using MvcSocialWeb.Middlewares.Services;
using MvcSocialWeb.ViewModels;
using System.Diagnostics;

namespace MvcSocialWeb.Controllers
{
    /// <summary>
    /// Контроллер аккаунтов
    /// </summary>
    public class AccountManagerController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ControllerServices _controllerServices;

        public AccountManagerController(UserManager<User> userManager, SignInManager<User> signInManager, ControllerServices controllerServices)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _controllerServices = controllerServices;
        }

        /// <summary>
        /// Страница авторизации    (не реализовано*)
        /// </summary>
        [Route("Login")]
        [HttpGet]
        public IActionResult Login()
        {
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Авторизация пользователя
        /// </summary>
        [Route("Login")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.UserEmail);
                var result = await _signInManager.PasswordSignInAsync(user?.UserName ?? string.Empty, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                        return Redirect(model.ReturnUrl);
                    else
                        return RedirectToAction("MyPage");
                }
                else
                    ModelState.AddModelError("", "Неправильный email и (или) пароль");
            }
            return View("/Views/Home/Index.cshtml", new AccountView(model));
        }

        /// <summary>
        /// Выход пользователя с сайта
        /// </summary>
        [Route("Logout")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout(string returnUrl)
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home", new { returnUrl });
        }

        /// <summary>
        /// Страница пользователя
        /// </summary>
        [HttpGet]
        [Route("MyPage")]
        [Authorize]
        public async Task<IActionResult> MyPage()
        {
            var user = User;
            var result = await _userManager.GetUserAsync(user);
            var friends = await _controllerServices.GetFriends(user);

            var model = new UserViewModel(result!, friends);

            return View("User", model);
        }

        /// <summary>
        /// Страница редактирования профиля
        /// </summary>
        [HttpGet]
        [Route("Edit")]
        [Authorize]
        public async Task<IActionResult> Edit()
        {
            ViewData["User"] = await _userManager.GetUserAsync(User);
            return View();
        }

        /// <summary>
        /// Редактирование учетной записи
        /// </summary>
        [HttpPost]
        [Route("Update")]
        [Authorize]
        public async Task<IActionResult> Update(UserEditViewModel model)
        {
            var user = User;
            var currentUser = await _userManager.GetUserAsync(user);

            await _controllerServices.CheckDataAtUpdate(this, model.UserName, model.Email, currentUser!);

            if (ModelState.IsValid)
            {
                currentUser!.Convert(model);

                var result = await _userManager.UpdateAsync(currentUser!);
                if (result.Succeeded)
                    return RedirectToAction("MyPage", "AccountManager");
                else
                    return RedirectToAction("Edit", "AccountManager");
            }
            else
            {
                ModelState.AddModelError("", "Некорректные данные");
                return View("Edit", model);
            }
        }

        /// <summary>
        /// Вывод страницы с ошибкой
        /// </summary>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
