using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MvcSocialWeb.Data.DBModel;
using MvcSocialWeb.ViewModels.Users;
using MvcSocialWeb.ViewModels.Account;

namespace MvcSocialWeb.Controllers
{
    /// <summary>
    /// Контроллер аккаунтов
    /// </summary>
    public class AccountManagerController : Controller
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountManagerController(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        /// <summary>
        /// Показ формы авторизации пользователя
        /// </summary>
        [Route("Login")]
        [HttpGet]
        public IActionResult Login()
        {
            return View("Home/Login");
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
                var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                        return Redirect(model.ReturnUrl);
                    else
                        return RedirectToAction("MyPage", new UserViewModel(user));
                }
                else
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
            }
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Выход пользователя с сайта
        /// </summary>
        [Route("Logout")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Страница пользователя
        /// </summary>
        [HttpGet]
        [Route("MyPage")]
        [Authorize]
        public IActionResult MyPage()
        {
            var result = _userManager.GetUserAsync(User);
            return View("User", new UserViewModel(result.Result));
        }

        /// <summary>
        /// Редактирование учетной записи
        /// </summary>
        [HttpGet]
        [Route("Edit")]
        [Authorize]
        public IActionResult Edit()
        {
            return View();
        }
    }
}
