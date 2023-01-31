using MvcSocialWeb.Data.DBModel.Users;

namespace MvcSocialWeb.ViewModels.Users
{
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
