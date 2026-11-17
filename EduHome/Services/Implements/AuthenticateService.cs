
using EduHome.Models.BaseModel;
using EduHome.Services.Interfaces;
using EduHome.ViewModels.Authentication;
using Microsoft.AspNetCore.Identity;

namespace EduHome.Services.Implements
{
    public class AuthenticationService : IAuthenticationService
    {
        readonly UserManager<BaseUser> _userManager;
        readonly SignInManager<BaseUser> _signManager;
        readonly IHttpContextAccessor _acc;
        readonly IEmailService _emailService;

        public AuthenticationService(UserManager<BaseUser> userManager, SignInManager<BaseUser> signManager, IHttpContextAccessor acc, IEmailService emailService)
        {
            _userManager = userManager;
            _signManager = signManager;
            _acc = acc;
            _emailService = emailService;
        }

        public async Task LoginAsync(LoginVM vm)
        {
            var user = await _userManager.FindByNameAsync(vm.Username)
                       ?? await _userManager.FindByEmailAsync(vm.Username);

            if (user == null)
                throw new Exception("Username or password is not correct!");

            var result = await _signManager.PasswordSignInAsync(user, vm.Password, true, false);
            if (!result.Succeeded)
                throw new Exception("Username or password is not correct!");
        }

        public async Task LogoutAsync()
        {
            await _signManager.SignOutAsync();
        }

        public async Task<string> ForgotPasswordAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) throw new Exception("User not found!");

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var link = $"{_acc.HttpContext.Request.Scheme}://{_acc.HttpContext.Request.Host}/Authenticate/ResetPassword?email={Uri.EscapeDataString(user.Email)}&token={Uri.EscapeDataString(token)}";

            await _emailService.SendEmailAsync(user.Email, "Reset Password", $"Plese, click on following link: \n{link}", false);

            return $"Email was sent to {email}";
        }

        public async Task ResetPasswordAsync(string email, string token, string newPassword)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) throw new Exception("User not found");
            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
            if (!result.Succeeded) throw new Exception("Reset password failed");
        }
    }
}