using EduHome.Areas.Teacher.ViewModels.Teacher;
using EduHome.Contexts;
using EduHome.Extensions;
using EduHome.Models;
using EduHome.Models.BaseModel;
using EduHome.Services.Interfaces;
using EduHome.ViewModels.Teacher;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EduHome.Services.Implements
{
    public class TeacherService : ITeacherService
    {
        readonly UserManager<BaseUser> _userManager;
        readonly IWebHostEnvironment _env;
        readonly IEmailService _emailService;
        readonly IOtpService _otpService;


        public TeacherService(UserManager<BaseUser> userManager, IWebHostEnvironment env, IEmailService emailService, IOtpService otpService)
        {
            _userManager = userManager;
            _env = env;
            _emailService = emailService;
            _otpService = otpService;
        }
        public async Task<List<TeacherGetAllVM>> GetAllAsync()
        {
            var teachers = await _userManager.Users.OfType<Teacher>().AsNoTracking().ToListAsync();
            var vms = teachers.Select(teacher => new TeacherGetAllVM
            {
                Id = teacher.Id,
                Firstname = teacher.Firstname,
                Lastname = teacher.Lastname,
                Image = teacher.Image,
                Speciality = teacher.Speciality
            }).ToList();
            return vms;
        }

        public async Task<TeacherGetSingleVM> GetSingleAsync(string id)
        {
            var teacher = await _userManager.Users.OfType<Teacher>().FirstOrDefaultAsync(t => t.Id == id);
            if (teacher == null) throw new Exception("Teacher not found!");
            var vm = new TeacherGetSingleVM
            {
                Id = teacher.Id,
                Firstname = teacher.Firstname,
                Lastname = teacher.Lastname,
                Image = teacher.Image,
                Speciality = teacher.Speciality,
                Degree = teacher.Degree,
                Description = teacher.Description,
                Email = teacher.Email,
                ExperienceInYear = teacher.ExperienceInYear,
                Faculty = teacher.Faculty,
                Phone = teacher.PhoneNumber
            };
            return vm;
        }

        public async Task RegisterAsync(TeacherRegisterVM vm)
        {
            var teacher = new Teacher
            {
                Degree = vm.Degree,
                Description = vm.Description,
                Email = vm.Email,
                ExperienceInYear = vm.ExperienceInYear,
                Faculty = vm.Faculty,
                Firstname = vm.Firstname,
                Lastname = vm.Lastname,
                PhoneNumber = vm.PhoneNumber,
                Speciality = vm.Specialty,
                UserName = vm.Username,
                Image = vm.Image.UploadFile(_env.WebRootPath, "images/teacher")
            };
            var result = await _userManager.CreateAsync(teacher, vm.Password);
            if (!result.Succeeded) throw new Exception("RegisterAsync user failed!");
            result = await _userManager.AddToRoleAsync(teacher, "Teacher");
            if (!result.Succeeded) throw new Exception("Add role failed!");
            var otp = _otpService.GenerateOtp(teacher.Email);
            await _emailService.SendEmailAsync(teacher.Email, "Verify Account", $"Your otp code is :{otp}", false);
        }

        public async Task RemoveAccountAsync(string id)
        {
            var teacher = await _userManager.Users.OfType<Teacher>().FirstOrDefaultAsync(t => t.Id == id);
            if (teacher == null) throw new Exception("Teacher not found!");

            var path = $"{_env.WebRootPath}/images/user/{teacher.Image}";
            if (File.Exists(path)) File.Delete(path);

            var result = await _userManager.DeleteAsync(teacher);
            if (!result.Succeeded) throw new Exception(result.Errors.FirstOrDefault().Description);
        }

        public async Task UpdateAsync(string id, TeacherUpdateVM vm)
        {
            var teacher = await _userManager.Users.OfType<Teacher>().FirstOrDefaultAsync(t => t.Id == id);
            if (teacher == null) throw new Exception("Teacher not found!");

            if (vm.Image != null)
            {
                var path = $"{_env.WebRootPath}/images/user/{teacher.Image}";
                if (File.Exists(path)) File.Delete(path);
                teacher.Image = vm.Image.UploadFile(_env.WebRootPath, "images/teacher");
            }
            teacher.Faculty = vm.Faculty;
            teacher.Firstname = vm.Firstname;
            teacher.Lastname = vm.Lastname;
            teacher.Degree = vm.Degree;
            teacher.Description = vm.Description;
            teacher.Speciality = vm.Speciality;
            teacher.ExperienceInYear = (byte)vm.ExperienceInYear;

            var result = await _userManager.UpdateAsync(teacher);
            if (!result.Succeeded) throw new Exception(result.Errors.FirstOrDefault().Description);
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

    }
}
