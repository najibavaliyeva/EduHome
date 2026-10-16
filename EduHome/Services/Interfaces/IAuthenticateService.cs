using EduHome.ViewModels.Authentication;

namespace EduHome.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task LoginAsync(LoginVM vm);
    }
}