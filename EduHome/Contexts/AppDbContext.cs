using EduHome.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using EduHome.Models.BaseModel;
namespace EduHome.Contexts
{
    public class AppDbContext : IdentityDbContext<BaseUser, Role, string>
    {
        public DbSet<Slider> sliders { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<Blog> blogs { get; set; }
        public DbSet<Course> courses { get; set; }
        public AppDbContext(DbContextOptions options) : base(options) 
        { 

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<BaseUser>()
                .HasDiscriminator<string>("Discriminator")
                .HasValue<Teacher>("teacher")
                .HasValue<AppUser>("user");
            base.OnModelCreating(builder);
        }
    }
}
