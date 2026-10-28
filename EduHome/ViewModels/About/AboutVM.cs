using EduHome.ViewModels.Teacher;

namespace EduHome.ViewModels.About
{
    public record AboutVM
    {
        public ICollection<TeacherGetAllVM> Teachers { get; set; }
    }
}
