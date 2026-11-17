using EduHome.ViewModels.Authentication;

namespace EduHome.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task LoginAsync(LoginVM vm);
        Task LogoutAsync();
        Task ResetPasswordAsync(string email, string token, string newPassword);
    }
}