using System.ComponentModel.DataAnnotations;

namespace MvcSocialWeb.ViewModels.Account
{
    /// <summary>
    /// Модель данных регистрации нового пользователя
    /// </summary>
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Поле обязательно для заполнения!")]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Поле обязательно для заполнения!")]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Поле обязательно для заполнения!")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string EmailReg { get; set; }

        [Required]
        [Display(Name = "Год")]
        public int? Year { get; set; }

        [Required]
        [Display(Name = "День")]
        public int? Date { get; set; }

        [Required]
        [Display(Name = "Месяц")]
        public Months? Month { get; set; }

        [Required(ErrorMessage = "Поле обязательно для заполнения!")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        [StringLength(100, ErrorMessage = "Поле {0} должно иметь минимум {2} и максимум {1} символов", MinimumLength = 5)]
        public string PasswordReg { get; set; }

        [Required(ErrorMessage = "Поле обязательно для заполнения!")]
        [Compare("PasswordReg", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердить пароль")]
        public string PasswordConfirm { get; set; }

        [Required(ErrorMessage = "Поле обязательно для заполнения!")]
        [Display(Name = "Никнейм")]
        public string Login { get; set; }

    }
}
