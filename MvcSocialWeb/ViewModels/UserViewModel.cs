using MvcSocialWeb.Data.DBModel;

namespace MvcSocialWeb.ViewModels
{
    public class UserViewModel
    {
        public User User { get; set; }

        public UserViewModel(User user)
        {
            User = user;
        }
    }
}
