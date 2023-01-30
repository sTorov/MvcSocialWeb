using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MvcSocialWeb.Data.DBModel;
using MvcSocialWeb.ViewModels.Users;
using MvcSocialWeb.ViewModels.Account;
using MvcSocialWeb.Middlewares.Extensions;


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

        [HttpGet]
        [Route("Edit")]
        [Authorize]
        public IActionResult Edit()
        {
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
            var user = await _userManager.GetUserAsync(User);

            var checkName = _userManager.FindByNameAsync(model.UserName).Result?.UserName;
            if (checkName != null && user.UserName != checkName)
                ModelState.AddModelError(string.Empty, $"Пользователь никнеймом '{model.UserName}' уже существует!");

            var checkEmail = _userManager.FindByEmailAsync(model.Email).Result?.Email;
            if (checkEmail != null && user.Email != checkEmail)
                ModelState.AddModelError(string.Empty, $"Адрес '{model.Email}' уже зарегистрирован!");

            if (ModelState.IsValid)
            {
                user.Convert(model);

                var result = await _userManager.UpdateAsync(user);
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
    }
}
