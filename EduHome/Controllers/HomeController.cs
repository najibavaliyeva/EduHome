using EduHome.Migrations;
using EduHome.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EduHome.Controllers
{
    public class HomeController : Controller
    {
        readonly ISliderService _sliderService;

        public HomeController(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }

        public IActionResult Index()
        {
            var vms = _sliderService.GetAll();
            return View(vms);
         
        }

    }
}
