using EduHome.Areas.Admin.ViewModels.Blog;
using EduHome.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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


        public IActionResult Index()
        {
           var vms = _service.GetAll();
            return View(vms);
        }
        public IActionResult Create()
        {
            var categories = _categoryService.GetAll();
            ViewBag.Categories = categories
                  .Select(x => new SelectListItem
                  {
                      Value = x.Id.ToString(),
                      Text = x.Name,
                  });
            return View();
        }
        [HttpPost]
        public IActionResult Create(BlogCreateVM vm)
        {
            if(!ModelState.IsValid) return View(vm);
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
            var getVM = _service.GetSingle(id);
            var vm = new BlogUpdateVM
            {
                ImageName = getVM.Image,
                Text = getVM.Text,
                Title = getVM.Title,

            }; return View(vm);
        }
        [HttpPost]
        public IActionResult Update(int id , BlogUpdateVM vm)
        {
            if (!ModelState.IsValid) return View(vm);
            _service.Update(id,vm);
            return RedirectToAction(nameof(Index));
        }
    }
}
