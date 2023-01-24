using Microsoft.AspNetCore.Identity;

namespace MvcSocialWeb.Data.DBModel
{
    /// <summary>
    /// Модель пользователя
    /// </summary>
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
