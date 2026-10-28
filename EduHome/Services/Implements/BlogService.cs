using EduHome.Areas.Admin.ViewModels.Blog;
using EduHome.Contexts;
using EduHome.Enums;
using EduHome.Extensions;
using EduHome.Models;
using EduHome.Models.BaseModel;
using EduHome.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EduHome.Services.Implements
{
    public class BlogService: IBlogService
    {
         private readonly AppDbContext _context;
       private  readonly IWebHostEnvironment _env;
        private readonly UserManager<BaseUser> _userManager;

        public BlogService(AppDbContext context, IWebHostEnvironment env, UserManager<BaseUser> userManager)
        {
            _context = context;
            _env = env;
            _userManager = userManager;
        }


        public async Task CreateAsync(BlogCreateVM vm)
        {

            var category = await _context.categories.FindAsync(vm.CategoryId);
            if (category == null) throw new Exception("Category not found");
            var teacher = await _userManager.FindByIdAsync(vm.TeacherId);
            if (teacher == null) throw new Exception("Teacher not found");

            if (!vm.Image.IsSizeValid(2, FileSize.MB)) throw new Exception("Size is not valid!");
            if (!vm.Image.IsFormatValid()) throw new Exception("Format is not valid");
            var blog = new Blog
            {
                Text = vm.Text,
                Title = vm.Title,
                Image = vm.Image.UploadFile(_env.WebRootPath, "images/blog"),
                CreatedAt = DateTime.UtcNow.AddHours(3),
                CategoryId = category.Id,
                TeacherId = teacher.Id
            };
            var entry = await _context.blogs.AddAsync(blog);
            if (entry.State != EntityState.Added) throw new Exception("Add failed");
            var count = await _context.SaveChangesAsync();
            if (count <= 0) throw new Exception("Save failed");
            
        }

        public async Task<List<BlogGetVM>> GetAllAsync()
        {
            var blogs =  _context.blogs.AsNoTracking().Include(b => b.Category).Include(b=> b.Teacher).AsQueryable();
            var vms = await blogs.Select(blog => new BlogGetVM
            {
                Id = blog.Id,
                CreatedAt = blog.CreatedAt,
                Image = blog.Image,
                Title = blog.Title,
                Text = blog.Text,
                CategoryName = blog.Category.Name,
                TeacherName = blog.Teacher.UserName,
                UpdatedAt = blog.UpdatedAt
            }).ToListAsync();
            return vms;

        }

        public async Task<BlogGetVM> GetSingleAsync(int id)
        {
            var blog = await _context.blogs.AsNoTracking().Include(b => b.Category).Include(b=>b.Teacher).FirstOrDefaultAsync(b => b.Id == id);
            if (blog == null) throw new Exception("Blog not found");
            var vm = new BlogGetVM
            {
                Id = blog.Id,
                CreatedAt = blog.CreatedAt,
                Image = blog.Image,
                Title = blog.Title,
                Text = blog.Text,
                CategoryName = blog.Category.Name,
                TeacherName = blog.Teacher.UserName, 
                UpdatedAt = blog.UpdatedAt

            }; return vm;    
        }

        public async Task RemoveAsync(int id)
        {
            var blog = await _context.blogs.FindAsync( id);
            if (blog == null) throw new Exception("Blog not found");
            var path = $"{_env.WebRootPath}/images/blog{blog.Image}";
            if(File.Exists(path)) File.Delete(path);

            var entry = _context.blogs.Remove(blog);
            if (entry.State != EntityState.Deleted) throw new Exception("RemoveAsync failed");
            var count = await _context.SaveChangesAsync();
            if (count <= 0) throw new Exception("Save failed");
        }

        public async Task UpdateAsync(int id, BlogUpdateVM vm)
        {
            var blog = await _context.blogs.FindAsync(id);
            if (blog == null) throw new Exception("Blog not found");
            if (vm.Image != null) {
                var path = $"{_env.WebRootPath}/images/blog{blog.Image}";
                if (File.Exists(path)) File.Delete(path);
                blog.Image = vm.Image.UploadFile(_env.WebRootPath, "images/blog");
            }
            blog.Title = vm.Title;
            blog.Text = vm.Text;
            blog.UpdatedAt = DateTime.UtcNow.AddHours(3);


            var entry = _context.blogs.Update(blog);
            if (entry.State != EntityState.Modified) throw new Exception("Update failed");
            var count = await _context.SaveChangesAsync();
            if (count <= 0) throw new Exception("Save failed");
        }
    }
}
