using EduHome.Services.Interfaces;
using EduHome.ViewModels.Student;
using EduHome.ViewModels.user;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EduHome.Controllers
{
    public class StudentController : Controller
    {
        readonly IStudentService _service;

        public StudentController(IStudentService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var vm = await _service.GetSingleAsync(userId);
            return View(vm);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(StudentRegisterVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            await _service.RegisterUserAsync(vm);
            return RedirectToAction(nameof(VerifyStudentEmail), new { email = vm.Email });
        }

        public IActionResult VerifyStudentEmail(string email)
        {
            ViewBag.Email = email;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> VerifyStudentEmail(OtpVM vm)
        {
            var result = await _service.VerifyAccountAsync(vm.Email, vm.Otp);
            return result ? RedirectToAction("Login", "Authenticate") : BadRequest("Failed!");
        }
    }
}