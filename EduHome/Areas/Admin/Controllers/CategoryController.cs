using EduHome.Areas.Admin.ViewModels.Category;
using EduHome.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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

        public async Task<IActionResult> Index()
        {
            var vms = await _service.GetAllAsync();
            return View(vms);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateVM vm)
        {
           if(!ModelState.IsValid) return View(vm);
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
            var category = await _service.GetSingleAsync(id);
            var vm = new CategoryUpdateVM
            {
                Name = category.Name
            }; return View(vm);  
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, CategoryUpdateVM vm)
        {
            if (!ModelState.IsValid) return View(vm);
            await _service.UpdateAsync(id, vm);
            return RedirectToAction(nameof(Index));
        }
    }
}
