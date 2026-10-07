using EduHome.Areas.Admin.ViewModels.Course;
using EduHome.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Create(CourseCreateVM vm)
        {
            if(!ModelState.IsValid)  return View(vm);
            await _service.CreateAsync(vm);
            return RedirectToAction(nameof(Index));
        }
         [HttpPost]
         public async Task<IActionResult> Remove(int id)
        {
            await _service.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update( int id)
        {
            var getVM = await _service.GetSingleAsync(id);
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
        public async Task<IActionResult> Update(int id, CourseUpdateVM vm)
        {
            if (!ModelState.IsValid) return View(vm);
            await _service.UpdateAsync(id,vm);
            return RedirectToAction(nameof(Index));
        }
    }
}
    
