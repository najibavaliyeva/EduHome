using EduHome.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EduHome.Controllers
{
    public class InstructorController : Controller
    {
        readonly ITeacherService _service;

        public InstructorController(ITeacherService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var vms = await _service.GetAllAsync();
            return View(vms);
        }

        public async Task<IActionResult> Details(string id)
        {
            var vm = await _service.GetSingleAsync(id);
            return View(vm);
        }
    }
}