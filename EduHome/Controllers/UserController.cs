using EduHome.Services.Interfaces;
using EduHome.ViewModels.user;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EduHome.Controllers
{
    public class UserController : Controller
    {
        readonly ITeacherService _teacherService;

        public UserController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        public IActionResult TeacherRegister()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> TeacherRegister(TeacherRegisterVM vm)
        {
           if (!ModelState.IsValid) return View(vm);
           await _teacherService.Register(vm);
            return RedirectToAction("Index ", "Home");
        }

        public IActionResult UserRegister()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UserRegister(AppUserRegisterVM vm)
        {
            if (!ModelState.IsValid) return View(vm);
            await _teacherService.RegisterUser(vm);
            return RedirectToAction("Index ", "Home");
        }
    }
}
