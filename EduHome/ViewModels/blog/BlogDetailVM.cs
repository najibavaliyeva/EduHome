using EduHome.Areas.Admin.ViewModels.Blog;
using EduHome.Areas.Admin.ViewModels.Category;

namespace EduHome.ViewModels.blog
{
    public class BlogDetailVM
    {
        public ICollection<CategoryGetVM> Categories { get; set; } = new List<CategoryGetVM>();
        public BlogGetVM Blog { get; set; }
    }
}
