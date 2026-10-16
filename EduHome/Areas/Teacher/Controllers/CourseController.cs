using EduHome.Areas.Admin.ViewModels.Course;
using EduHome.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EduHome.Areas.Teacher.Controllers
{
    [Area("Teacher")]

    
        public class CourseController : Controller
        {
            private readonly ICourseService _service;
            private readonly ICategoryService _categoryService;
            private readonly ITeacherService _teacherService;

            public CourseController(ICourseService service, ICategoryService categoryService, ITeacherService teacherService)
            {
                _service = service;
                _categoryService = categoryService;
                _teacherService = teacherService;
            }
            public async Task<IActionResult> Index()
            {
                var vms = await _service.GetAllAsync();
                return View(vms);
            }
            public async Task<IActionResult> Create()
            {

                var categories = await _categoryService.GetAllAsync();
                var teachers = await _teacherService.GetAllAsync();
                ViewBag.Categories = categories
                      .Select(x => new SelectListItem
                      {
                          Value = x.Id.ToString(),
                          Text = x.Name,
                      });
                ViewBag.Teachers = teachers
                      .Select(x => new SelectListItem
                      {
                          Value = x.Id.ToString(),
                          Text = $"{x.Firstname} {x.Lastname}",
                      });
                return View();
            }
            [HttpPost]
            public async Task<IActionResult> Create(CourseCreateVM vm)
            {
                if (!ModelState.IsValid)
                {
                    var categories = await _categoryService.GetAllAsync();
                    var teachers = await _teacherService.GetAllAsync();
                    ViewBag.Categories = categories
                     .Select(x => new SelectListItem
                     {
                         Value = x.Id.ToString(),
                         Text = x.Name,
                     });
                    ViewBag.Teachers = teachers
                          .Select(x => new SelectListItem
                          {
                              Value = x.Id.ToString(),
                              Text = $"{x.Firstname} {x.Lastname}",
                          });
                    return View(vm);
                }
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
                await _service.UpdateAsync(id, vm);
                return RedirectToAction(nameof(Index));
            }
        }
    }

