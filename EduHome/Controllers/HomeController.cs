using EduHome.Services.Interfaces;
using EduHome.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;

namespace EduHome.Controllers
{
    public class HomeController : Controller
    {
        readonly ISliderService _sliderService;
        readonly IBlogService _blogService;
        readonly ICourseService _courseService;
        public HomeController(ISliderService sliderService, IBlogService blogService, ICourseService courseService)
        {
            _sliderService = sliderService;
            _blogService = blogService;
            _courseService = courseService;
        }
        public async Task<IActionResult> Index()
        {
            var sliders = await _sliderService.GetAllAsync();
            var blogs = await _blogService.GetAllAsync();
            var courses = await _courseService.GetAllAsync();
            var homeVm = new HomeVM
            {
                Blogs = blogs,
                Sliders = sliders,
                Courses = courses
            };
            return View(homeVm);
        }
    }
}