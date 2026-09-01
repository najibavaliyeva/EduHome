using EduHome.Contexts;
using EduHome.Models;
using EduHome.Services.Interfaces;
using EduHome.ViewModels.Slider;
using Microsoft.AspNetCore.Mvc;

namespace EduHome.Areas.Admin.Controllers
{
    [Area("admin")]
    public class SliderController : Controller
    {
        readonly ISliderService _service;
        public SliderController(ISliderService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            var vms = _service.GetAll();
            return View(vms);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(SliderCreateVM vm)
        {
            if (!ModelState.IsValid) return View(vm);
            _service.Create(vm);

            return RedirectToAction(nameof(Index));

        }
        [HttpPost]
        public IActionResult Remove(int id)
        {
            _service.Remove(id);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Update(int id)
        {
            var slider = _service.GetSingle(id);
            var vm = new SliderUpdateVM
            {
                ImageName = slider.Image,
                Text = slider.Text,
                Title = slider.Title,
            };
            return View(vm);
        }
        [HttpPost]
        public IActionResult Update(int id, SliderUpdateVM vm)
        {
            if(!ModelState.IsValid) return View(vm);
            _service.Update(id, vm);
            return RedirectToAction(nameof(Index));

        }
    }

}
