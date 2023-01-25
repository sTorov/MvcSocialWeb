using System.ComponentModel.DataAnnotations;

namespace MvcSocialWeb.ViewModels.Account
{
    /// <summary>
    /// Модель авторизации пользователя
    /// </summary>
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string UserEmail { get; set; }

        [Required]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Запомнть?")]
        public bool RememberMe { get; set; }

        public string? ReturnUrl { get; set; }
    }
}
