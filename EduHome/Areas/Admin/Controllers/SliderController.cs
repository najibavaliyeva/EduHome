using EduHome.Areas.Admin.ViewModels.Slider;
using EduHome.Contexts;
using EduHome.Models;
using EduHome.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
            var vms = _service.GetAllAsync();
            return View(vms);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SliderCreateVM vm)
        {
            if (!ModelState.IsValid) return View(vm);
            await _service.CreateAsync(vm);

            return RedirectToAction(nameof(Index));

        }
        [HttpPost]
        public async Task<IActionResult> Remove(int id)
        {
            await _service.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int id)
        {
            var slider = await _service.GetSingleAsync(id);
            var vm = new SliderUpdateVM
            {
                ImageName = slider.Image,
                Text = slider.Text,
                Title = slider.Title,
            };
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, SliderUpdateVM vm)
        {
            if(!ModelState.IsValid) return View(vm);
            await _service.UpdateAsync(id, vm);
            return RedirectToAction(nameof(Index));

        }
    }

}
