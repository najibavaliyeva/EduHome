using EduHome.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EduHome.Areas.Teacher.Controllers
{
    [Area("teacher")]
    public class BlogController : Controller
    {
        readonly IBlogService _service;
        readonly ICategoryService _categoryService;

        readonly ITeacherService _teacherService;
        public BlogController(IBlogService service, ICategoryService categoryService, ITeacherService teacherService)
        {
            _service = service;
            _categoryService = categoryService;
            _teacherService = teacherService;
        }

        public async Task<IActionResult> Index()
        { 
            return View();
        }
        public async Task<IActionResult> Create()
        {
            var categories = await _categoryService.GetAllAsync();
            var teachers = await _teacherService.GetAllAsync();
            ViewBag.Categories = categories
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                });
            return View();
        }
    }
}
