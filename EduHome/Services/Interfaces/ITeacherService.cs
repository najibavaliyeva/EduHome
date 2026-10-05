using EduHome.ViewModels.user;

namespace EduHome.Services.Interfaces
{
    public interface ITeacherService
    {
        Task UserRegister(AppUserRegisterVM vm);
        Task Register(TeacherRegisterVM vm);
    }
}
