
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

        public AuthenticationService(UserManager<BaseUser> userManager, SignInManager<BaseUser> signManager)
        {
            _userManager = userManager;
            _signManager = signManager;
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
    }
}