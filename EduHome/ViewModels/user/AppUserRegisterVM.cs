using System.ComponentModel.DataAnnotations;

namespace EduHome.ViewModels.user
{
    public class AppUserRegisterVM
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
      public DateOnly Birthdate { get; set; }
        public string University { get; set; }
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = " You have to enter phone number")]
        [RegularExpression(@"^(?:\+994|0)(?:10|50|51|55|70|77|99|60|12)\d{7}$",
       ErrorMessage = "Enter valid Azerbaijanian number  (f.e: +994501234567 or 0501234567).")]
        public string PhoneNumber { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}

