using EduHome.Models;
using Microsoft.EntityFrameworkCore;

namespace EduHome.Contexts
{
    public class AppDbContext : DbContext
    {
        internal readonly object Sliders;

        public DbSet<Slider> sliders { get; set; }
        public AppDbContext(DbContextOptions options) : base(options) 
        { 

        }
    }
}
