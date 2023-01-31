using Microsoft.AspNetCore.Identity;

namespace MvcSocialWeb.Data.DBModel.Users
{
    /// <summary>
    /// Модель пользователя
    /// </summary>
    public class User : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? MiddleName { get; set; }
        public DateTime BirthDate { get; set; }
        public string? Image { get; set; }
        public string? Status { get; set; }
        public string? About { get; set; }

        public User()
        {
            Image = "https://via.placeholder.com/500";
            Status = "Ура! Я в соцсети!";
            About = "Информация обо мне.";
        }

        public string GetFullName() => $"{FirstName} {MiddleName} {LastName}";

        public string GetNormalizedAbout() => $"\"{About}\"";
    }
}
