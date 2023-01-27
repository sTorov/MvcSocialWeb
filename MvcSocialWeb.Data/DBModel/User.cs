using Microsoft.AspNetCore.Identity;

namespace MvcSocialWeb.Data.DBModel
{
    /// <summary>
    /// Модель пользователя
    /// </summary>
    public class User : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? MiddleName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Image { get; set; }
        public string Status { get; set; }
        public string About { get; set; }

        public User() { }

        public User(string image, string status, string about)
        {
            Image = image;
            Status = status;
            About = about;
        }

        public string GetFullName() => $"{FirstName} {MiddleName} {LastName}";
    }
}
