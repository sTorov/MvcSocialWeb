using MvcSocialWeb.Data.DBModel.Users;
using MvcSocialWeb.ViewModels.Users;

namespace MvcSocialWeb.Middlewares.Extensions
{
    /// <summary>
    /// Расширения для модели пользователя
    /// </summary>
    public static class UserExtensions
    {
        /// <summary>
        /// Преобразование модели представления редактирования пользователя в модель пользователя
        /// </summary>
        public static User Convert(this User user, UserEditViewModel model)
        {
            user.FirstName = model.FirstName;
            user.MiddleName = model.MiddleName;
            user.LastName = model.LastName;
            user.UserName = model.UserName;
            user.Email = model.Email;
            user.BirthDate = model.BirthDate;
            user.Status = model.Status;
            user.About = model.About?.ReplaceQuotes();
            user.Image = model.Image;

            return user;
        }
    }
}
