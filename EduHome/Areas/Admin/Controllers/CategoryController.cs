using EduHome.Services.Interfaces;
using EduHome.ViewModels.Category;
using Microsoft.AspNetCore.Mvc;

namespace EduHome.Areas.Admin.Controllers
{
    [Area("admin")]
    public class CategoryController : Controller
    {
        readonly ICategoryService _service;

        public CategoryController(ICategoryService service)
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
        public IActionResult Create(CategoryCreateVM vm)
        {
           if(!ModelState.IsValid) return View(vm);
           _service.Create(vm);
            return RedirectToAction(nameof(Index));
        }

    }
}
