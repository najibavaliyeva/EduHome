using Microsoft.AspNetCore.Mvc;

namespace EduHome.Controllers
{
    public class InstructorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
