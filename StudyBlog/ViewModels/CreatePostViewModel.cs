using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace StudyBlog.ViewModels
{
    public class CreatePostViewModel
    {
        [Required(ErrorMessage = "Поле описание обязательно для заполнения")]
        [MinLength(10, ErrorMessage = "Минимальная длина описания 10 символов")]
        [Display(Name = "Описание")]
        [DataType(DataType.Text)]
        public string Description { get; set; }
        
        [Required(ErrorMessage = "Загрузка фото обязательна")]
        [DataType(DataType.Upload)]
        [Display(Name = "Изображение публикации")]
        public IFormFile File { get; set; }
    }
}