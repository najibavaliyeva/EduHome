using EduHome.Areas.Admin.ViewModels.Blog;
using EduHome.Areas.Admin.ViewModels.Course;
using EduHome.Areas.Admin.ViewModels.Slider;

namespace EduHome.ViewModels.Home
{
    public record HomeVM
    {
        public ICollection<SliderGetVM> Sliders { get; set; }
        public ICollection<BlogGetVM> Blogs { get; set; }
        public ICollection<CourseGetVM> Courses { get; set; }
    }
}