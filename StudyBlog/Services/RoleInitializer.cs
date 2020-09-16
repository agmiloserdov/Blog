using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using StudyBlog.Models;

namespace StudyBlog.Services
{
    public class RoleInitializer
    {
        public static async Task Initialize(
            RoleManager<IdentityRole> roleManager,
            UserManager<User> userManager)
        {
            string adminEmail = "admin@admin.com";
            string adminPassword = "1qaz@WSX29";

            var roles = new[] {"admin", "user"};

            foreach (var role in roles)
            {
                if (await roleManager.FindByNameAsync(role) is null)
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

            if (await userManager.FindByNameAsync(adminEmail) is null)
            {
                User admin = new User
                {
                    Email = adminEmail,
                    UserName = adminEmail
                };
                var result = await userManager.CreateAsync(admin, adminPassword);
                if (result.Succeeded)
                    await userManager.AddToRoleAsync(admin, "admin");

            }
        }
    }
}