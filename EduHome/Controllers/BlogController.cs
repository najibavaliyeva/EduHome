using AspNetCoreGeneratedDocument;
using EduHome.Services.Interfaces;
using EduHome.ViewModels.blog;
using Microsoft.AspNetCore.Mvc;

namespace EduHome.Controllers
{
    public class BlogController : Controller
    {
        readonly ICategoryService _categoryService;
        readonly IBlogService _blogService;

        public BlogController(ICategoryService categoryService, IBlogService blogService)
        {
            this._categoryService = categoryService;
            this._blogService = blogService;
        }

        public IActionResult Index()
        {
            var vm = new BlogVM
            {
                Blogs = _blogService.GetAll(),
                Categories = _categoryService.GetAll(),

            }; return View(vm);
        } 

        public IActionResult Details(int id)
        {
            var blog = _blogService.GetSingle(id);
            var vm = new BlogDetailVM
            {
                Blog = blog,
                Categories = _categoryService.GetAll(),
            };
            return View(vm);
        }
    }
}
