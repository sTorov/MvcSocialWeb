using System.ComponentModel.DataAnnotations;

namespace MvcSocialWeb.ViewModels.Users
{
    public class UserEditViewModel
    {
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Display(Name = "Отчество")]
        public string? MiddleName { get; set; }

        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [Display(Name = "Никнейм")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [DataType(DataType.Date)]
        [Display(Name = "Дата рождения")]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Фото")]
        [DataType(DataType.ImageUrl)]
        public string? Image { get; set; }

        [Display(Name = "Статус")]
        public string? Status { get; set; }

        [Display(Name = "О себе")]
        public string? About { get; set; }
    }
}
