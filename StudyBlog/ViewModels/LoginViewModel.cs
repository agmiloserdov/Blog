using System.ComponentModel.DataAnnotations;

namespace StudyBlog.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Это поле обязательно")]
        [Display(Name = "Электронная почта")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        
        
        [Required(ErrorMessage = "Это поле обязательно")]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Запомнить меня")]
        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }
}