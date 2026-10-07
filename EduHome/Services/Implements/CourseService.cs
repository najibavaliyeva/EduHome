using EduHome.Areas.Admin.ViewModels.Course;
using EduHome.Contexts;
using EduHome.Enums;
using EduHome.Extensions;
using EduHome.Models;
using EduHome.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace EduHome.Services.Implements
{
    public class CourseService : ICourseService
    {
        readonly IWebHostEnvironment _env;
        readonly AppDbContext _context;

        public CourseService(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task CreateAsync(CourseCreateVM vm)
        {
            var category = await _context.categories.FindAsync(vm.CategoryId);
            if (category == null) throw new Exception("Category not found");

            if (!vm.Image.IsSizeValid(2, FileSize.MB)) throw new Exception("Size is not valid!");
            if (!vm.Image.IsFormatValid()) throw new Exception("Format is not valid");

            var course = new Course
            {
                CategoryId = category.Id,
                Description = vm.Description,
                DurationInMonth = vm.DurationInMonth,
                Image = vm.Image.UploadFile(_env.WebRootPath, "images/course"),
                Info = vm.Info,
                ClassDurationInHours = vm.ClassDurationInHours,
                Language = vm.Language,
                Price = vm.Price,
                IsSelfAssesment = vm.IsSelfAssesment,
                SkillLevel = vm.SkillLevel,
                StartsAt = vm.StartsAt,
                CreatedAt = DateTime.UtcNow.AddHours(3),
                StudentCapacity = vm.StudentCapacity,
                Title = vm.Title
            };
            var entry = await _context.courses.AddAsync(course);
            if (entry.State != EntityState.Added) throw new Exception("Add failed");
            var count = await _context.SaveChangesAsync();
            if (count <= 0) throw new Exception("Save failed");

        }

        public async Task<List<CourseGetVM>> GetAllAsync()
        {
            var course = await _context.courses.AsNoTracking().Include(c =>c.Category).ToListAsync();
            var vms = course.Select(course => new CourseGetVM
            {
                ClassDurationInHours = course.ClassDurationInHours,
                Description = course.Description,
                DurationInMonth = course.DurationInMonth,
                Info = course.Info,
                Language = course.Language.ToString(),
                Price = course.Price,
                IsSelfAssesment = course.IsSelfAssesment,
                SkillLevel = course.SkillLevel.ToString(),
                StartsAt = course.StartsAt,
                Title = course.Title,
                Image = course.Image,
                StudentCapacity = course.StudentCapacity,
                CategoryId = course.CategoryId,
                Level = course.SkillLevel,
                LanguageValue = course.Language,
                Id = course.Id
            }).ToList();
            return vms;
        }

        public async Task<CourseGetVM> GetSingleAsync(int id)
        {
           var course = await _context.courses.AsNoTracking().FirstAsync( course =>  course.Id== id  );
            if (course == null) throw new Exception("Course not found");
            var vm = new CourseGetVM
            {
                Description = course.Description,
                DurationInMonth = course.DurationInMonth,
                Info = course.Info,
                Price = course.Price,
                IsSelfAssesment = course.IsSelfAssesment,
                CategoryId = course.CategoryId,
                SkillLevel = course.SkillLevel.ToString(),
                StartsAt = course.StartsAt,
                ClassDurationInHours = course.ClassDurationInHours,
                StudentCapacity = course.StudentCapacity,
                Language = course.Language.ToString(),
                Title = course.Title,
                Image = course.Image,
                Id = course.Id,
                Level = course.SkillLevel,
                LanguageValue = course.Language
            };
            return vm;
            
        }

        public async Task RemoveAsync(int id)
        {
            var course = await _context.courses.FindAsync(id);
            if (course == null) throw new Exception("Course not found");
            var path = $"{_env.WebRootPath}/images/course/{course.Image}";
            if(File.Exists(path)) File.Delete(path);
             var entry =  _context.courses.Remove(course);
            if (entry.State != EntityState.Deleted) throw new Exception("RemoveAsync failed");
            var count = await _context.SaveChangesAsync();
            if (count <= 0) throw new Exception("Save failed");
        
        }

        public async Task UpdateAsync(int id, CourseUpdateVM vm)
        {

            var course = await _context.courses.FindAsync(id);
            if (course == null) throw new Exception("Course not found");
            if (vm.Image != null)
            {
                var path = $"{_env.WebRootPath}/images/course/{course.Image}";
                if (File.Exists(path)) File.Delete(path);
                course.Image = vm.Image.UploadFile(_env.WebRootPath, "images/course");
            }
            course.Title = vm.Title;
            course.Price =(decimal)vm.Price;
            course.StudentCapacity = (ushort)vm.StudentCapacity;
            course.UpdatedAt = DateTime.UtcNow.AddHours(3);
            course.ClassDurationInHours = (byte)vm.ClassDurationInHours;
            course.SkillLevel =(SkillLevel)vm.SkillLevel;
            course.DurationInMonth = (byte)vm.DurationInMonth;
            course.Info = vm.Info;
            course.Description = vm.Description;
          //  course.IsSelfAssesment = (bool)vm.IsSelfAssesment;
            course.StartsAt = (DateOnly)vm.StartsAt;
            
            var entry = _context.courses.Update(course);
            if (entry.State != EntityState.Modified) throw new Exception("Update failed");
            var count = await _context.SaveChangesAsync();
            if (count <= 0) throw new Exception("Save failed");
        }
    }
}
