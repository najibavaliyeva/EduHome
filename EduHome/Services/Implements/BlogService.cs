using EduHome.Areas.Admin.ViewModels.Blog;
using EduHome.Contexts;
using EduHome.Extensions;
using EduHome.Models;
using EduHome.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

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


        public void Create(BlogCreateVM vm)
        {
            var category = _context.categories.Find(vm.CategoryId);
            if (category == null) throw new Exception("Category not found");
            var blog = new Blog
            {
                Text = vm.Text,
                Title = vm.Title,
                Image = vm.Image.UploadFile(_env.WebRootPath, "images/blog"),
                CreatedAt = DateTime.UtcNow.AddHours(3),
                CategoryId = category.Id,
            };
            var entry = _context.blogs.Add(blog);
            if (entry.State != EntityState.Added) throw new Exception("Add failed");
            var count = _context.SaveChanges();
            if (count <= 0) throw new Exception("Save failed");
            
        }

        public List<BlogGetVM> GetAll()
        {
            var blog = _context.blogs.AsNoTracking().Include(b => b.Category). ToList();
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

        public BlogGetVM GetSingle(int id)
        {
            var blog = _context.blogs.AsNoTracking().Include(b => b.Category).FirstOrDefault(b => b.Id == id);
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

        public void Remove(int id)
        {
            var blog = _context.blogs.Find( id);
            if (blog == null) throw new Exception("Blog not found");
            var path = $"{_env.WebRootPath}/images/blog{blog.Image}";
            if(File.Exists(path)) File.Delete(path);

            var entry = _context.blogs.Remove(blog);
            if (entry.State != EntityState.Deleted) throw new Exception("Remove failed");
            var count = _context.SaveChanges();
            if (count <= 0) throw new Exception("Save failed");
        }

        public void Update(int id, BlogUpdateVM vm)
        {
            var blog = _context.blogs.Find(id);
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
            var count = _context.SaveChanges();
            if (count <= 0) throw new Exception("Save failed");
        }
    }
}
