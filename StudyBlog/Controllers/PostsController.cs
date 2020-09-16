using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using StudyBlog.Models;
using StudyBlog.Services;
using StudyBlog.ViewModels;

namespace StudyBlog.Controllers
{
    
    public class PostsController : Controller
    {
        private BlogContext _db;
        private readonly IHostEnvironment _environment; 
        private readonly UploadFileService _uploadFileService;
        private UserManager<User> _userManager;

        public PostsController(BlogContext db, IHostEnvironment environment, UploadFileService uploadFileService, UserManager<User> userManager)
        {
            _db = db;
            _environment = environment;
            _uploadFileService = uploadFileService;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            List<Post> posts = _db.Posts.Where(p => !p.IsDeleted).ToList();
            return View(posts);
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }
        
        public async Task<IActionResult> Post(string id)
        {
            var post = await _db.Posts.FirstOrDefaultAsync(p => p.Id == id);
            if (post != null)
                return View(post);
            return NotFound();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreatePostViewModel model)
        {
            if (ModelState.IsValid)
            {
                string path = Path.Combine(_environment.ContentRootPath,"wwwroot/images/userPosts/");
                string photoPath = $"images/userPosts/{model.File.FileName}";
                _uploadFileService.Upload(path, model.File.FileName, model.File);
                Post post = new Post()
                {
                    Description = model.Description,
                    PhotoPath = photoPath,
                    UserId = _userManager.GetUserId(User)
                };
                var result = _db.Posts.AddAsync(post);
                if (result.IsCompleted)
                {
                    await _db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                    
            }
            return View(model);
        }

        [Authorize]
        public IActionResult Edit(string id)
        {
            Post post = _db.Posts.FirstOrDefault(p => p.Id == id);
            if (post == null) return NotFound();
            if (post.UserId != _userManager.GetUserId(User)) return Forbid();
            var model = new EditPostViewModel()
            {
                Id = post.Id,
                Description = post.Description,
                PhotoPath = post.PhotoPath
            };
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(EditPostViewModel model)
        {
            var post = _db.Posts.FirstOrDefault(p => p.Id == model.Id);
            if (post == null) return NotFound();
            if (post.UserId != _userManager.GetUserId(User)) return Forbid();
            if (model.File != null)
            {
                string path = Path.Combine(_environment.ContentRootPath,"wwwroot/images/userPosts/");
                string photoPath = $"images/userPosts/{model.File.FileName}";
                _uploadFileService.Upload(path, model.File.FileName, model.File);
                post.PhotoPath = photoPath;
            }
            else
                post.PhotoPath = model.PhotoPath;
            post.Description = model.Description;
            _db.Entry(post).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Post", new{id = post.Id});
        }
        
//TODO : Реализовать удаление для сущности поста!
//        public IActionResult Delete(string id)
//        {
//            
//        }
    }
}