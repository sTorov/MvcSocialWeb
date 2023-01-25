using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MvcSocialWeb.Data.DBModel;

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
    }
}
