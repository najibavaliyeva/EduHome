using EduHome.Areas.Admin.ViewModels.Blog;
using EduHome.Areas.Admin.ViewModels.Category;

namespace EduHome.ViewModels
{
    public class BlogVM
    {
         public ICollection<BlogGetVM> Blogs { get; set; }  = new List<BlogGetVM>();    
        public ICollection<CategoryGetVM> Categories { get; set; } = new List<CategoryGetVM>();
    }
}
