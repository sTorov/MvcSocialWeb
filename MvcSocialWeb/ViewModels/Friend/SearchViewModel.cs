using MvcSocialWeb.Data.DBModel.Users;

namespace MvcSocialWeb.ViewModels.Friend
{
    public class SearchViewModel
    {
        public List<UserWithFriendExt> FindUsers { get; set; }

        public SearchViewModel()
        {
            FindUsers= new List<UserWithFriendExt>();
        }
    }
}
