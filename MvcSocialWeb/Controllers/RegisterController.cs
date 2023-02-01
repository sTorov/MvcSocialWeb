using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MvcSocialWeb.Data.DBModel.Users;
using MvcSocialWeb.ViewModels;
using MvcSocialWeb.ViewModels.Account;
using MvcSocialWeb.ViewModels.Users;
using System.Diagnostics;

namespace MvcSocialWeb.Controllers
{
    /// <summary>
    /// Контроллер регистрации
    /// </summary>
    public class RegisterController : Controller
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public RegisterController(IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        /// Первый этап регистрации
        /// </summary>
        [Route("Register")]
        [HttpGet]
        public IActionResult Register()
        {
            return View("Home/Register");
        }

        /// <summary>
        /// Второй этап регистрации
        /// </summary>
        [Route("RegisterPart2")]
        [HttpGet]
        public IActionResult RegisterPart2(RegisterViewModel model)
        {
            return View("RegisterPart2", model);
        }

        /// <summary>
        /// Регистрация нового пользователя в базе данных
        /// </summary>
        [Route("Register")]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            await CheckUserData(model.Login, model.EmailReg);

            if (ModelState.IsValid) 
            {
                var user = _mapper.Map<User>(model);
                var result = await _userManager.CreateAsync(user, model.PasswordReg);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user,  false);
                    return RedirectToAction("MyPage", "AccountManager");
                }
                else
                {
                    foreach (var error in result.Errors)
                        ModelState.AddModelError(string.Empty, error.Description);                    
                }
            }
            return View("RegisterPart2", model);
        }

        /// <summary>
        /// Вывод страницы с ошибкой
        /// </summary>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        /// <summary>
        /// Проверка введённых пользователем данных на существование в БД
        /// </summary>
        private async Task CheckUserData(string login, string email)
        {
            var checkName = (await _userManager.FindByNameAsync(login))?.UserName;
            if (checkName != null)
                ModelState.AddModelError(string.Empty, $"Пользователь с никнеймом {login} уже существует!");

            var checkEmail = (await _userManager.FindByEmailAsync(email))?.Email;
            if (checkEmail != null)
                ModelState.AddModelError(string.Empty, $"Адрес {email} уже зарегистрирован!");
        }
    }
}
