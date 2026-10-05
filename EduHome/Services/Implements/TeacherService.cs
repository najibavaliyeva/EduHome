using EduHome.Contexts;
using EduHome.Extensions;
using EduHome.Models;
using EduHome.Models.BaseModel;
using EduHome.Services.Interfaces;
using EduHome.ViewModels.user;
using Microsoft.AspNetCore.Identity;

namespace EduHome.Services.Implements
{
    public class TeacherService:ITeacherService
    {
        readonly UserManager<BaseUser> _userManager;
        readonly IWebHostEnvironment _env;

        public TeacherService(UserManager<BaseUser> userManager, IWebHostEnvironment env)
        {
            _userManager = userManager;
            _env = env;  
        }

        public async Task Register(TeacherRegisterVM vm)
        {
            var teacher = new Teacher
            {

                Email = vm.Email,
                Firstname = vm.Firstname,
                Lastname = vm.Lastname,
                Description = vm.Description,
                Faculty = vm.Faculty,
                UserName = vm.Username,
                Degree = vm.Degree,
                PhoneNumber = vm.PhoneNumber,
                Speciality = vm.Specialty,
                ExperienceInYear = vm.ExperienceInYear,
                Image = vm.Image.UploadFile(_env.WebRootPath, "images/teacher"),
               CreatedAt = DateTime.UtcNow.AddHours(4),
            };
          var result =  await _userManager.CreateAsync(teacher, vm.Password);
            if (!result.Succeeded) throw new Exception("Create succeeded");
            result = await _userManager.AddToRoleAsync(teacher, "teacher");
            if (!result.Succeeded) throw new Exception("Add role failed");
        }

        public async Task UserRegister(AppUserRegisterVM vm)
        {
            var user = new AppUser
            {

                Email = vm.Email,
                Firstname = vm.Firstname,
                Lastname = vm.Lastname,
                UserName = vm.Username,
                PhoneNumber = vm.PhoneNumber,
                BirthDate  = vm.Birthdate,
                Universty = vm.University,
                CreatedAt = DateTime.UtcNow.AddHours(4),
            };
            var result = await _userManager.CreateAsync(user ,vm.Password );
            if (!result.Succeeded) throw new Exception("Create succeeded");
            result = await _userManager.AddToRoleAsync(user, "Student");
            if (!result.Succeeded) throw new Exception("Add role failed");
        }

    }
}
