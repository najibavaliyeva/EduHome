using EduHome.Areas.Admin.ViewModels.Category;
using EduHome.Contexts;
using EduHome.Migrations;
using EduHome.Models;
using EduHome.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EduHome.Services.Implements
{
    public class CategoryService : ICategoryService
    {
        readonly AppDbContext _context;

        public CategoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(CategoryCreateVM vm)
        {
            var category = new Category
            {
                Name = vm.Name,
                CreatedAt = DateTime.UtcNow.AddHours(3)
            };
            var entry = await _context.categories.AddAsync(category);
            if (entry.State != EntityState.Added) throw new Exception("Add failed");
            var count = await _context.SaveChangesAsync();
            if (count <= 0) throw new Exception("Save failed");
        }

        public async Task<List<CategoryGetVM>> GetAllAsync()
        {
           var categories = await _context.categories.AsNoTracking().ToListAsync();
            var vms = categories.Select(category => new CategoryGetVM
            {
                Name = category.Name,
                CreatedAt = category.CreatedAt,
                Id = category.Id,
                UpdatedAt = category.UpdatedAt
            }).ToList(); 
               return vms;
        }

        public async Task<CategoryGetVM> GetSingleAsync(int id)
        {
            var category = await _context.categories.AsNoTracking().FirstOrDefaultAsync(category => category.Id == id);
            if (category == null) throw new Exception("Category not found!");
            var vm = new CategoryGetVM
            {
                Name = category.Name,
                CreatedAt = category.CreatedAt,
                Id = category.Id,
                UpdatedAt = category.UpdatedAt
            }; return vm;
        }

        public async Task RemoveAsync(int id)
        {
            var category = await _context.categories.FindAsync(id);
            if (category == null) throw new Exception("Category not found!");

            var entry = _context.Remove(category);
            if (entry.State != EntityState.Deleted) throw new Exception("RemoveAsync failed");
            var count = await _context.SaveChangesAsync();
            if (count <= 0) throw new Exception("Save failed!");

        }

        public async Task UpdateAsync(int id, CategoryUpdateVM vm)
        {
            var category = await _context.categories.FindAsync(id);
            if (category == null) throw new Exception("Category not found!");
            category.Name = vm.Name;
            category.UpdatedAt = DateTime.UtcNow.AddHours(3);
            var entry = _context.Update(category);
            if (entry.State != EntityState.Modified) throw new Exception("Update failed");
            var count = await _context.SaveChangesAsync();
            if (count <= 0) throw new Exception("Save failed");
        }
    }
}
