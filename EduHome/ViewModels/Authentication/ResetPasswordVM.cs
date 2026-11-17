using System.ComponentModel.DataAnnotations;

namespace EduHome.ViewModels.Authentication
{
    public record ResetPasswordVM
    {

        [EmailAddress]
        public string Email { get; set; }
        public string Token { get; set; }
        public string Password { get; set; }
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
