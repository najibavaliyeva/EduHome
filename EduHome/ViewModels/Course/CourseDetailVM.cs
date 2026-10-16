using EduHome.Areas.Admin.ViewModels.Category;
using EduHome.Areas.Admin.ViewModels.Course;

namespace EduHome.ViewModels.Course
{
    internal class CourseDetailVM
    {
        public ICollection<CategoryGetVM> Categories { get; set; }
        public CourseGetVM Course { get; set; }
    }
}