using Microsoft.AspNetCore.Identity;
using MvcSocialWeb.Controllers;
using MvcSocialWeb.Data.DBModel.Users;

namespace MvcSocialWeb.Middlewares.Services
{
    /// <summary>
    /// Проверка введённых пользователем данных на существование в БД
    /// </summary>
    public class UserValidation
    {
        private readonly UserManager<User> _userManager;

        public UserValidation(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// Проверка введённых пользователем данных на существование в БД при регистрации
        /// </summary>
        public async Task CheckDataAtRegistration(RegisterController controller, string login, string email)
        {
            var checkName = (await _userManager.FindByNameAsync(login))?.UserName;
            if (checkName != null)
                controller.ModelState.AddModelError(string.Empty, $"Пользователь с никнеймом {login} уже существует!");

            var checkEmail = (await _userManager.FindByEmailAsync(email))?.Email;
            if (checkEmail != null)
                controller.ModelState.AddModelError(string.Empty, $"Адрес {email} уже зарегистрирован!");
        }

        /// <summary>
        /// Проверка введённых данных пользователем на существование в БД при обновлении профиля
        /// </summary>
        public async Task CheckDataAtUpdate(AccountManagerController controller,string login, string email, User user)
        {
            var checkName = (await _userManager.FindByNameAsync(login))?.UserName;
            if (checkName != null && user.UserName != checkName)
                controller.ModelState.AddModelError(string.Empty, $"Пользователь с никнеймом '{login}' уже существует!");

            var checkEmail = (await _userManager.FindByEmailAsync(email))?.Email;
            if (checkEmail != null && user.Email != checkEmail)
                controller.ModelState.AddModelError(string.Empty, $"Адрес '{email}' уже зарегистрирован!");
        }
    }
}
