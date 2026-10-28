using EduHome.Services.Interfaces;
using EduHome.ViewModels.Teacher;
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
           await _teacherService.RegisterAsync(vm);
            return RedirectToAction(nameof(VerifyEmail), new { email = vm.Email });
        }

        public IActionResult VerifyEmail(string email)
        {
            ViewBag.Email = email;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> VerifyEmail(OtpVM vm)
        {
            var result = await _teacherService.VerifyAccountAsync(vm.Email, vm.Otp);
            return result ? RedirectToAction("Login", "Authenticate") : BadRequest("Failed!");
        }

    }
}
