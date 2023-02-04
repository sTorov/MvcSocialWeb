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
            Image = "https://thispersondoesnotexist.com/image";
            Status = "Ура! Я в соцсети!";
            About = "Информация обо мне.";
        }

        /// <summary>
        /// Получение польного имени пользователя
        /// </summary>
        public string GetFullName() => $"{FirstName} {MiddleName} {LastName}";

        /// <summary>
        /// Получение значения поля About в двойных кавычках
        /// </summary>
        public string GetNormalizedAbout() => $"\"{About}\"";
    }
}
