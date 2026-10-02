using EduHome.Areas.Admin.ViewModels.Course;
using EduHome.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EduHome.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CourseController : Controller
    {

        private readonly ICourseService _service;
        private readonly ICategoryService _categoryService;

        public CourseController(ICourseService service, ICategoryService categoryService)
        {
            _service = service;
            _categoryService = categoryService;
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
        public IActionResult Create(CourseCreateVM vm)
        {
            if(!ModelState.IsValid)  return View(vm);
            _service.Create(vm);
            return RedirectToAction(nameof(Index));
        }
         [HttpPost]
         public IActionResult Remove(int id)
        {
            _service.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update( int id)
        {
            var getVM = _service.GetSingle(id);
            var vm = new CourseUpdateVM
            {
                Info = getVM.Info,
                ClassDurationInHours = getVM.ClassDurationInHours,
                Description = getVM.Description,
                DurationInMonth = getVM.DurationInMonth,
                ImageName = getVM.Image,
                 //IsSelfAssesment = getVM.IsSelfAssesment,
                Language = getVM.LanguageValue,
                Price = getVM.Price,
                SkillLevel = getVM.Level,
                StartsAt = getVM.StartsAt,
                StudentCapacity = getVM.StudentCapacity,
                Title = getVM.Title,
            }; return View(vm);
        }
        [HttpPost]
        public IActionResult Update(int id, CourseUpdateVM vm)
        {
            if (!ModelState.IsValid) return View(vm);
            _service.Update(id,vm);
            return RedirectToAction(nameof(Index));
        }
    }
}
    
