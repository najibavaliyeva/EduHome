using EduHome.ViewModels.Student;

namespace EduHome.Services.Interfaces
{
    public interface IStudentService
    {
        Task<StudentGetVM> GetSingleAsync(string id);
        Task RegisterUserAsync(StudentRegisterVM vm);
        Task<bool> VerifyAccountAsync(string email, int otp);
        //update
        //changepassword
    }

}