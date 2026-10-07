using EduHome.Areas.Admin.ViewModels.Blog;
using EduHome.Contexts;
using EduHome.Enums;
using EduHome.Extensions;
using EduHome.Models;
using EduHome.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EduHome.Services.Implements
{
    public class BlogService: IBlogService
    {
         private readonly AppDbContext _context;
       private  readonly IWebHostEnvironment _env;

        public BlogService(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }


        public async Task CreateAsync(BlogCreateVM vm)
        {

            var category = await _context.categories.FindAsync(vm.CategoryId);
            if (category == null) throw new Exception("Category not found");

            if (!vm.Image.IsSizeValid(2, FileSize.MB)) throw new Exception("Size is not valid!");
            if (!vm.Image.IsFormatValid()) throw new Exception("Format is not valid");
            var blog = new Blog
            {
                Text = vm.Text,
                Title = vm.Title,
                Image = vm.Image.UploadFile(_env.WebRootPath, "images/blog"),
                CreatedAt = DateTime.UtcNow.AddHours(3),
                CategoryId = category.Id,
            };
            var entry = await _context.blogs.AddAsync(blog);
            if (entry.State != EntityState.Added) throw new Exception("Add failed");
            var count = await _context.SaveChangesAsync();
            if (count <= 0) throw new Exception("Save failed");
            
        }

        public async Task<List<BlogGetVM>> GetAllAsync()
        {
            var blog = await _context.blogs.AsNoTracking().Include(b => b.Category). ToListAsync();
            var vms = blog.Select(blog => new BlogGetVM
            {
                Id = blog.Id,
                CreatedAt = blog.CreatedAt,
                Image = blog.Image,
                Title = blog.Title,
                Text = blog.Text,
                CategoryName = blog.Category.Name
            }).ToList();
            return vms;

        }

        public async Task<BlogGetVM> GetSingleAsync(int id)
        {
            var blog = await _context.blogs.AsNoTracking().Include(b => b.Category).FirstOrDefaultAsync(b => b.Id == id);
            if (blog == null) throw new Exception("Blog not found");
            var vm = new BlogGetVM
            {
                Id = blog.Id,
                CreatedAt = blog.CreatedAt,
                Image = blog.Image,
                Title = blog.Title,
                Text = blog.Text,
                CategoryName = blog.Category.Name

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
