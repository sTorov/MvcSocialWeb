using MvcSocialWeb.Data.DBModel.Users;

namespace MvcSocialWeb.ViewModels.Friend
{
    /// <summary>
    /// Модель данных страницы поиска
    /// </summary>
    public class SearchViewModel
    {
        public List<UserWithFriendExt> FindUsers { get; set; }

        public SearchViewModel()
        {
            FindUsers= new List<UserWithFriendExt>();
        }
    }
}
