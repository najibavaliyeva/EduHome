using AspNetCoreGeneratedDocument;
using EduHome.Services.Interfaces;
using EduHome.ViewModels.blog;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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

        public async Task<IActionResult> Index()
        {
            var vm = new BlogVM
            {
                Blogs = await _blogService.GetAllAsync(),
                Categories = await _categoryService.GetAllAsync(),

            }; return View(vm);
        } 

        public async Task<IActionResult> Details(int id)
        {
            var blog = await _blogService.GetSingleAsync(id);
            var vm = new BlogDetailVM
            {
                Blog = blog,
                Categories = await _categoryService.GetAllAsync(),
            };
            return View(vm);
        }
    }
}
