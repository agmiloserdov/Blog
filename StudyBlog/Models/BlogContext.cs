using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace StudyBlog.Models
{
    public class BlogContext : IdentityDbContext<User>
    {
        public override DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }

        public BlogContext(DbContextOptions options) : base(options)
        {
        }
    }
}