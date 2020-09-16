using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace StudyBlog.ViewModels
{
    public class EditPostViewModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "Поле описание обязательно для заполнения")]
        [MinLength(10, ErrorMessage = "Минимальная длина описания 10 символов")]
        [Display(Name = "Описание")]
        [DataType(DataType.Text)]
        public string Description { get; set; }
        public string PhotoPath { get; set; }
        
        [DataType(DataType.Upload)]
        [Display(Name = "Изображение публикации")]
        public IFormFile File { get; set; }
    }
}