using EduHome.Areas.Admin.ViewModels.Blog;
using EduHome.Areas.Admin.ViewModels.Course;

namespace EduHome.Services.Interfaces
{
    public interface ICourseService
    {
        //create
        Task CreateAsync(CourseCreateVM vm);

        //remove
        Task RemoveAsync(int id);
        //update
        Task UpdateAsync(int id, CourseUpdateVM vm);
        //getall

        Task <List<CourseGetVM>> GetAllAsync();
        //getsingle
        Task <CourseGetVM> GetSingleAsync(int id);
    }
}
