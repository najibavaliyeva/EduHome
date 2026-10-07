using EduHome.Areas.Admin.ViewModels.Blog;
using EduHome.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Threading.Tasks;

namespace EduHome.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class BlogController : Controller
    {
       private readonly IBlogService _service;
        private readonly ICategoryService _categoryService;

        public BlogController(ICategoryService categoryService, IBlogService service)
        {
            _categoryService = categoryService;
            _service = service;
        }


        public async Task<IActionResult> Index()
        {
           var vms = await _service.GetAllAsync();
            return View(vms);
        }
        public async Task<IActionResult> Create()
        {
            var categories = await _categoryService.GetAllAsync();
            ViewBag.Categories = categories
                  .Select(x => new SelectListItem
                  {
                      Value = x.Id.ToString(),
                      Text = x.Name,
                  });
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(BlogCreateVM vm)
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
            var getVM = await _service.GetSingleAsync(id);
            var vm = new BlogUpdateVM
            {
                ImageName = getVM.Image,
                Text = getVM.Text,
                Title = getVM.Title,

            }; return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id , BlogUpdateVM vm)
        {
            if (!ModelState.IsValid) return View(vm);
            await _service.UpdateAsync(id,vm);
            return RedirectToAction(nameof(Index));
        }
    }
}
