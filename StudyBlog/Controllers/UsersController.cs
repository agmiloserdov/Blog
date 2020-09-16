using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudyBlog.Models;

namespace StudyBlog.Controllers
{
    [Authorize(Roles = "admin")]
    public class UsersController : Controller
    {
        private UserManager<User> _userManager;

        public UsersController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        // GET
        public IActionResult Index()
        {
            return View(_userManager.Users.ToList());
        }
    }
}