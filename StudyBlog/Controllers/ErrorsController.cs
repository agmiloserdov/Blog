using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace StudyBlog.Controllers
{
    public class ErrorsController : Controller
    {
        // GET
        public IActionResult Index(int code)
        {
            switch (code)
            {
                case (int)HttpStatusCode.NotFound :
                    return View("NotFound");
                default:
                    return RedirectToAction("Index", "Posts");
            }
        }

       
    }
}