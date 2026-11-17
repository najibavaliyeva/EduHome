using EduHome.Services.Interfaces;
using EduHome.ViewModels.Course;
using Microsoft.AspNetCore.Mvc;

namespace EduHome.Controllers
{
    public class CourseController : Controller
    {
        readonly ICourseService _courseService;
        readonly ICategoryService _categoryService;

        public CourseController(ICourseService courseService, ICategoryService categoryService)
        {
            _courseService = courseService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var vms = await _courseService.GetAllAsync();
            return View(vms);
        }

        public async Task<IActionResult> Details(int id)
        {
            var vm = await _courseService.GetSingleAsync(id);
            var categories = await _categoryService.GetAllAsync();
            var detailsVM = new CourseDetailVM
            {
                Course = vm,
                Categories = categories
            };
            return View(detailsVM);
        }
    }
}