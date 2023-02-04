namespace MvcSocialWeb.Data.DBModel.Users
{
    /// <summary>
    /// Модель пользователя, расширенная полем, которое содержит информацию о том, 
    /// является ли указанный пользователя другом для текущего
    /// </summary>
    public class UserWithFriendExt : User
    {
        public bool IsFriendWithCurrent { get; set; }
    }
}
