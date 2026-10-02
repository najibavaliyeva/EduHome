using EduHome.Areas.Admin.ViewModels.Blog;
using EduHome.Areas.Admin.ViewModels.Course;

namespace EduHome.Services.Interfaces
{
    public interface ICourseService
    {
        //create
        void Create(CourseCreateVM vm);

        //remove
        void Remove(int id);
        //update
        void Update(int id, CourseUpdateVM vm);
        //getall

        List<CourseGetVM> GetAll();
        //getsingle
        CourseGetVM GetSingle(int id);
    }
}
