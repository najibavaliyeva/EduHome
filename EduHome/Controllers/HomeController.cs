using EduHome.Contexts;
using EduHome.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EduHome.Controllers
{
    public class HomeController : Controller
    {
        private AppDbContext _context;
        readonly ISliderService _sliderService;
        readonly IBlogService _blogService;
        readonly ICourseService _courseService;

        public HomeController(ISliderService sliderService, IBlogService blogService, ICourseService courseService)
        {
            _sliderService = sliderService;
            _blogService = blogService;
            _courseService = courseService;
        }

        //public async Task<IActionResult> Index()
        //{
        //    var sliders = _context.sliders.ToList();
        //    return View(sliders);
        //    var sliders = await _sliderService.GetAllAsync();
        //    var blogs = await _blogService.GetAllAsync();
        //    var courses = await _courseService.GetAllAsync();
        //    var homeVm = new HomeVM
        //    {
        //        Blogs = blogs,
        //        Sliders = sliders,
        //        Courses = courses
        //    };
        //    return View(homeVm);
        //}
    }
}