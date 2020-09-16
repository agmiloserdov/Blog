using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace StudyBlog.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Это поле обязательно")]
        [Display(Name = "Электронная почта")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Это поле обязательно")]
        [Display(Name = "Дата рождения")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Это поле обязательно")]
        [Display(Name = "Имя")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }
        [Display(Name = "Фамилия")]
        [DataType(DataType.Text)]
        public string SecondName { get; set; }

        [Required(ErrorMessage = "Загрузка фото обязательна")]
        [DataType(DataType.Upload)]
        [Display(Name = "Фото профиля")]
        public IFormFile File { get; set; }
        
        [Required(ErrorMessage = "Это поле обязательно")]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [Required(ErrorMessage = "Это поле обязательно")]
        [Display(Name = "Введите пароль повторно")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }
    }
}