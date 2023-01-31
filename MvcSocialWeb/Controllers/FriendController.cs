using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MvcSocialWeb.Data.DBModel.Friend;
using MvcSocialWeb.Data.DBModel.Users;
using MvcSocialWeb.Data.Repositories;
using MvcSocialWeb.Data.Repositories.Interfaces;
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

        public FriendController(UserManager<User> userManager, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Поиск пользователей
        /// </summary>
        [HttpGet]
        [Route("Search")]
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
            
            return repo.GetFriendsByUser(result);
        }
    }
}
