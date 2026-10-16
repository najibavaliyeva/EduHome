using EduHome.Services.Interfaces;
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
    }
}