using System.ComponentModel.DataAnnotations;

namespace MvcSocialWeb.ViewModels.Messages
{
    /// <summary>
    /// Модель данных сообщения
    /// </summary>
    public class MessageViewModel
    {
        [Required]
        public string Text { get; set; }
    }
}
