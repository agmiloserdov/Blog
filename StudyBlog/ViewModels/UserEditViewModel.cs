using System;
using System.ComponentModel.DataAnnotations;

namespace StudyBlog.ViewModels
{
    public class UserEditViewModel
    {
        [Required(ErrorMessage = "Обязательно установите дату")]
        public DateTime? BirthDate { get; set; }
        [Required(ErrorMessage = "Обязательно укажите имя")]
        public string FirstName { get; set; }
        [MinLength(2, ErrorMessage = "Мнимальная длина 2 символа")]
        public string? SecondName { get; set; }

        public string Id { get; set; }
    }
}