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
using System.Security.Claims;

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
        private readonly UserServices _userServices;

        public FriendController(UserManager<User> userManager, IMapper mapper, IUnitOfWork unitOfWork, UserServices userServices)
        {
            _userManager = userManager;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userServices = userServices;
        }

        /// <summary>
        /// Добавление пользователя в друзья
        /// </summary>
        [HttpPost]
        [Route("AddFriend")]
        [Authorize]
        public async Task<IActionResult> AddFriend(string id)
        {
            var items = await _userServices.GetItemForManipulation<Friend, FriendRepository>(User, id);

            await items.repo!.AddFriend(items.user!, items.friend!);
            await items.repo!.AddFriend(items.friend!, items.user!);

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
            var items = await _userServices.GetItemForManipulation<Friend, FriendRepository>(User, id);

            await items.repo!.DeleteFriend(items.user!, items.friend!);
            await items.repo!.DeleteFriend(items.friend!, items.user!);

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
            var model = new SearchViewModel();
            if (string.IsNullOrEmpty(search) || string.IsNullOrWhiteSpace(search))
                return model;

            var currentUser = User;
            var result = await _userManager.GetUserAsync(currentUser);
            
            var list = _userManager.Users.AsEnumerable()
                .Where(user => user.GetFullName().ToLower().Contains(search.ToLower()))
                .ToList();
            var withFriend = await GetAllFriend();
            
            var data = new List<UserWithFriendExt>();
            foreach (var searchedUser in list)
            {
                var t = _mapper.Map<UserWithFriendExt>(searchedUser);
                t.IsFriendWithCurrent = withFriend
                    .Where(friend => friend.Id == searchedUser.Id).Count() != 0;

                if(t.Id != result.Id)
                    data.Add(t);
            }

            model.FindUsers = data;
            return model;
        }

        /// <summary>
        /// Получение списка друзей пользователя
        /// </summary>
        private async Task<List<User>> GetAllFriend()
        {
            var user = User;
            var result = await _userManager.GetUserAsync(user);
            var repo = _unitOfWork.GetRepository<Friend>() as FriendRepository;
            
            return await repo?.GetFriendsByUser(result);
        }
    }
}
