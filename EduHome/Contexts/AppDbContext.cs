using EduHome.Models;
using Microsoft.EntityFrameworkCore;

namespace EduHome.Contexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<Slider> sliders { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<Blog> blogs { get; set; }
        public AppDbContext(DbContextOptions options) : base(options) 
        { 

        }
    }
}
