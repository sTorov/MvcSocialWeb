using MvcSocialWeb.Data.DBModel.Users;

namespace MvcSocialWeb.ViewModels.Users
{
    /// <summary>
    /// Модель данных главной страницы пользователя
    /// </summary>
    public class UserViewModel
    {
        public User User { get; set; }
        public List<User> Friends { get; set; }

        public UserViewModel(User user, List<User> friends)
        {
            User = user;
            Friends = friends;
        }
    }
}
