using Microsoft.AspNetCore.Identity;
using MvcSocialWeb.Controllers;
using MvcSocialWeb.Data.DBModel.Friend;
using MvcSocialWeb.Data.DBModel.Users;
using MvcSocialWeb.Data.Repositories;
using MvcSocialWeb.Data.Repositories.Interfaces;
using System.Security.Claims;

namespace MvcSocialWeb.Middlewares.Services
{
    /// <summary>
    /// Общий функционал для контроллеров
    /// </summary>
    public class ControllerServices
    {
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public ControllerServices(UserManager<User> userManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
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

        /// <summary>
        /// Получение кортежа с текущим пользователем, пользователем с указанным id и репозиторием указанного типа
        /// </summary>
        public async Task<(User? user, User? friend, TRepository? repo)> GetItemForManipulation<TEntity, TRepository>(ClaimsPrincipal claims, string id) 
            where TEntity : class
            where TRepository : Repository<TEntity>

        {
            var currentUser = await _userManager.GetUserAsync(claims);
            var friend = await _userManager.FindByIdAsync(id);
            var repo = _unitOfWork.GetRepository<TEntity>() as TRepository;

            return (currentUser, friend, repo);
        }

        /// <summary>
        /// Получение списка друзей пользователя
        /// </summary>
        public async Task<List<User>> GetFriends(ClaimsPrincipal claims)
        {
            var currentUser = await _userManager.GetUserAsync(claims);
            var repo = _unitOfWork.GetRepository<Friend>() as FriendRepository;

            return (await repo!.GetFriendsByUserAsync(currentUser!)) ?? new List<User>();
        }
    }
}
