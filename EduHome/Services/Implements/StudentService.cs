using EduHome.Models;
using EduHome.Models.BaseModel;
using EduHome.Services.Interfaces;
using EduHome.ViewModels.Student;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EduHome.Services.Implements
{
    public class StudentService : IStudentService
    {
        readonly UserManager<BaseUser> _userManager;
        readonly IEmailService _emailService;
        readonly IOtpService _otpService;

        public StudentService(UserManager<BaseUser> userManager, IEmailService emailService, IOtpService otpService)
        {
            _userManager = userManager;
            _emailService = emailService;
            _otpService = otpService;
        }

        public async Task RegisterUserAsync(StudentRegisterVM vm)
        {
            var student = new AppUser
            {
                Email = vm.Email,
                Firstname = vm.Firstname,
                Lastname = vm.Lastname,
                PhoneNumber = vm.PhoneNumber,
                BirthDate = vm.Birthdate,
                Universty = vm.University,
                UserName = vm.Username
            };

            var result = await _userManager.CreateAsync(student, vm.Password);
            if (!result.Succeeded) throw new Exception(result.Errors.FirstOrDefault().Description);

            result = await _userManager.AddToRoleAsync(student, "Student");
            if (!result.Succeeded) throw new Exception(result.Errors.FirstOrDefault().Description);
            var otp = _otpService.GenerateOtp(student.Email);
            await _emailService.SendEmailAsync(student.Email, "Verify Account", $"Your OTP code is: {otp}/nThis code expires in 5 Minutes.", false);
        }

        public async Task<bool> VerifyAccountAsync(string email, int otp)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) throw new Exception("user not found!");

            var result = _otpService.VerifyOtp(email, otp);
            if (result)
            {
                user.EmailConfirmed = true;
                var iResult = await _userManager.UpdateAsync(user);
                if (!iResult.Succeeded) throw new Exception("Verify email failed");
            }
            return result;
        }

        public async Task<StudentGetVM> GetSingleAsync(string id)
        {
            var student = await _userManager.Users.OfType<AppUser>().AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
            if (student == null) throw new Exception("Student not found!");
            var vm = new StudentGetVM
            {
                Id = student.Id,
                BirthDate = student.BirthDate,
                Email = student.Email,
                FirstName = student.Firstname,
                LastName = student.Lastname,
                University = student.Universty
            };
            return vm;
        }
    }
}