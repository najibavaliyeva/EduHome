using EduHome.Contexts;
using EduHome.Models;
using EduHome.Services.Interfaces;
using EduHome.ViewModels.Category;
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
    }
}
