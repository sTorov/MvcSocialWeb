using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MvcSocialWeb.Data.DBModel.Friend;
using MvcSocialWeb.Data.DBModel.Users;
using MvcSocialWeb.Data.Repositories;
using MvcSocialWeb.Data.Repositories.Interfaces;
using MvcSocialWeb.Middlewares.Services;
using MvcSocialWeb.ViewModels.Friend;

namespace MvcSocialWeb.Controllers
{
    /// <summary>
    /// Контроллер друзей
    /// </summary>
    public class FriendController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ControllerServices _controllerServices;

        public FriendController(UserManager<User> userManager, IMapper mapper, IUnitOfWork unitOfWork, ControllerServices controllerServices)
        {
            _userManager = userManager;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _controllerServices = controllerServices;
        }

        /// <summary>
        /// Добавление пользователя в друзья
        /// </summary>
        [HttpPost]
        [Route("AddFriend")]
        [Authorize]
        public async Task<IActionResult> AddFriend(string id)
        {
            var (user, friend, repo) = await _controllerServices.GetItemForManipulation<Friend, FriendRepository>(User, id);

            await repo!.AddFriendAsync(user!, friend!);
            await repo!.AddFriendAsync(friend!, user!);

            return RedirectToAction("MyPage", "AccountManager");
        }

        /// <summary>
        /// Удаление пользователя из друзей
        /// </summary>
        [HttpPost]
        [Route("Delete")]
        [Authorize]
        public async Task<IActionResult> DeleteFriend(string id)
        {
            var (user, friend, repo) = await _controllerServices.GetItemForManipulation<Friend, FriendRepository>(User, id);

            await repo!.DeleteFriendAsync(user!, friend!);
            await repo!.DeleteFriendAsync(friend!, user!);

            return RedirectToAction("MyPage", "AccountManager");
        }

        /// <summary>
        /// Поиск пользователей
        /// </summary>
        [HttpGet]
        [Route("Search")]
        [Authorize]
        public async Task<IActionResult> UserList(string search)
        {
            var model = await CreateSearch(search);
            return View(model);
        }

        /// <summary>
        /// Получение списка всех найденых по запросу пользователей.
        /// Данный список содержит информацию о том, кто уже является другом для пользователя
        /// </summary>
        private async Task<SearchViewModel> CreateSearch(string search)
        {
            var currentUser = User;
            var result = await _userManager.GetUserAsync(currentUser);

            var list = _userManager.Users.AsEnumerable().ToList();
            if (!string.IsNullOrEmpty(search) || !string.IsNullOrWhiteSpace(search))
                list = list.Where(user => user.GetFullName().ToLower().Contains(search.ToLower())).ToList();

            var withFriend = await _controllerServices.GetFriends(currentUser);
            
            var data = new List<UserWithFriendExt>();
            foreach (var searchedUser in list)
            {
                var t = _mapper.Map<UserWithFriendExt>(searchedUser);
                t.IsFriendWithCurrent = withFriend
                    .Where(friend => friend.Id == searchedUser.Id).Count() != 0;

                if(t.Id != result!.Id)
                    data.Add(t);
            }

            var model = new SearchViewModel() { FindUsers = data };

            return model;
        }
    }
}
