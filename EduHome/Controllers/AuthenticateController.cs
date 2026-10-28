using EduHome.Services.Interfaces;
using EduHome.ViewModels.Authentication;
using EduHome.ViewModels.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace EduHome.Controllers
{
    public class AuthenticateController : Controller
    {
        readonly IAuthenticationService _service;

        public AuthenticateController(IAuthenticationService service)
        {
            _service = service;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM vm)
        {
            await _service.LoginAsync(vm);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _service.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordVM vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

        //    var result = await _service.F(vm.Email);


          //  TempData["SuccessMessage"] = result;

            return RedirectToAction(nameof(Login));
        }


        public IActionResult ResetPassword()
        {
            return View();
        }
    }
}