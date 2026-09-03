using EduHome.Contexts;
using EduHome.Migrations;
using EduHome.Models;
using EduHome.Services.Interfaces;
using EduHome.ViewModels.Category;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHome.Services.Implements
{
    public class CategoryService : ICategoryService
    {
        readonly AppDbContext _context;

        public CategoryService(AppDbContext context)
        {
            _context = context;
        }

        public void Create(CategoryCreateVM vm)
        {
            var category = new Category
            {
                Name = vm.Name,
                CreatedAt = DateTime.UtcNow.AddHours(3)
            };
            var entry = _context.categories.Add(category);
            if (entry.State != EntityState.Added) throw new Exception("Add failed");
            var count = _context.SaveChanges();
            if (count <= 0) throw new Exception("Save failed");
        }

        public List<CategoryGetVM> GetAll()
        {
           var categories = _context.categories.AsNoTracking().ToList();
            var vms = categories.Select(category => new CategoryGetVM
            {
                Name = category.Name,
                CreatedAt = category.CreatedAt,
                Id = category.Id,
                UpdatedAt = category.UpdatedAt
            }).ToList(); 
               return vms;
        }

        public CategoryGetVM GetSingle(int id)
        {
            var category = _context.categories.AsNoTracking().FirstOrDefault(category => category.Id == id);
            if (category == null) throw new Exception("Category not found!");
            var vm = new CategoryGetVM
            {
                Name = category.Name,
            }; return vm;
        }

        public void Remove(int id)
        {
            var category = _context.categories.Find(id);
            if (category == null) throw new Exception("Category not found!");

            var entry = _context.Remove(category);
            if (entry.State != EntityState.Deleted) throw new Exception("Remove failed");
            var count = _context.SaveChanges();
            if (count <= 0) throw new Exception("Save failed!");

        }

        public void Update(int id, CategoryUpdateVM vm)
        {
            var category = _context.categories.Find(id);
            if (category == null) throw new Exception("Category not found!");
            category.Name = vm.Name;
            category.UpdatedAt = DateTime.UtcNow.AddHours(3);
            var entry = _context.Update(category);
            if (entry.State != EntityState.Modified) throw new Exception("Update failed");
            var count = _context.SaveChanges();
            if (count <= 0) throw new Exception("Save failed");
        }
    }
}
