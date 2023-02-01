using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MvcSocialWeb.ViewModels.Users;
using MvcSocialWeb.ViewModels.Account;
using MvcSocialWeb.Middlewares.Extensions;
using MvcSocialWeb.Data.DBModel.Users;
using MvcSocialWeb.Data.Repositories.Interfaces;
using MvcSocialWeb.Data.DBModel.Friend;
using MvcSocialWeb.Data.Repositories;
using MvcSocialWeb.Middlewares.Services;

namespace MvcSocialWeb.Controllers
{
    /// <summary>
    /// Контроллер аккаунтов
    /// </summary>
    public class AccountManagerController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserServices _userValidation;

        public AccountManagerController(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper, IUnitOfWork unitOfWork, UserServices userValidation)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _unitOfWork = unitOfWork;
            _userValidation = userValidation;
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
        public async Task<IActionResult> MyPage()
        {
            var user = User;
            var result = await _userManager.GetUserAsync(user);
            var model = new UserViewModel(result!, await GetFriends());

            return View("User", model);
        }

        /// <summary>
        /// Страница редактирования профиля
        /// </summary>
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
            var user = User;
            var currentUser = await _userManager.GetUserAsync(user);

            await _userValidation.CheckDataAtUpdate(this, model.UserName, model.Email, currentUser!);

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
        /// Получение списка друзей пользователя
        /// </summary>
        private async Task<List<User>?> GetFriends()
        {
            var user = User;
            var currentUser = await _userManager.GetUserAsync(user);
            var repo = _unitOfWork.GetRepository<Friend>() as FriendRepository;

            return await repo?.GetFriendsByUser(currentUser);
        }
    }
}
