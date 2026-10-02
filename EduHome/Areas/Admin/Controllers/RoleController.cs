using EduHome.Areas.Admin.ViewModels.Role;
using EduHome.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EduHome.Areas.Admin.Controllers
{
    [Area("admin")]
    public class RoleController : Controller
    {
        readonly IRoleService _service;

        public RoleController(IRoleService service)
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
        public async Task<IActionResult> Create(RoleCreateVM vm)
        {
          await _service.CreateAsync(vm);
            return RedirectToAction ("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Remove(string id)
        {
            await _service.RemoveAsync(id);
            return RedirectToAction ("Index");
        }

    }
}
