using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace StudyBlog.Models
{
    public class User : IdentityUser
    {
        public DateTime? BirthDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string FirstName { get; set; }
        public string? SecondName { get; set; }
        public string AvatarPath { get; set; }

        public virtual List<Post> Posts { get; set; }
    }
}