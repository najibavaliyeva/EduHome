using EduHome.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EduHome.Areas.Admin.Controllers
{
    [Area("admin")]
    public class TeacherController : Controller
    {
        readonly ITeacherService _service;

        public TeacherController(ITeacherService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var vms = await _service.GetAllAsync();
            return View(vms);
        }
    }
}