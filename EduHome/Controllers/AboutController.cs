using EduHome.Services.Interfaces;
using EduHome.ViewModels.About;
using Microsoft.AspNetCore.Mvc;

namespace EduHome.Controllers
{
    public class AboutController : Controller
    {
        readonly ITeacherService _teacherService;

        public AboutController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        public async Task<IActionResult> Index()
        {
            var teachers = await _teacherService.GetAllAsync();
            var vm = new AboutVM
            {
                Teachers = teachers
            };
            return View(vm);
        }
    }
}
        