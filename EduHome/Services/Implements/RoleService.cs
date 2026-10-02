using EduHome.Areas.Admin.ViewModels.Role;
using EduHome.Models;
using EduHome.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EduHome.Services.Implements
{
    public class RoleService : IRoleService
    {
        readonly RoleManager<Role> _roleManager;

        public RoleService(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task CreateAsync(RoleCreateVM vm)
        {
            var role = new Role
            {
                Description = vm.Description,
                Name = vm.Name
            };
           var result= await _roleManager.CreateAsync(role);
            if (!result.Succeeded) throw new Exception("CreateAsync failed");
        }

        public async Task<List<RoleGetVM>> GetAllAsync()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            var vms = roles.Select(role=> new RoleGetVM
            {
                Description=role.Description,
                Name = role.Name,
                Id = role.Id
            }).ToList();
            return vms;
        }

        public async Task RemoveAsync(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null) throw new Exception("Role not found.");
            var result = await _roleManager.DeleteAsync(role);
            if (!result.Succeeded) throw new Exception("Delete failed.");
        }
    }
}
