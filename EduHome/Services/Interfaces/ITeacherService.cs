using EduHome.ViewModels.user;

namespace EduHome.Services.Interfaces
{
    public interface ITeacherService
    {
        Task RegisterUser(AppUserRegisterVM vm);
        Task Register(TeacherRegisterVM vm);
        Task RemoveAccount(string id);
        Task UpdateAsync(string id, Teac)
    }
}
