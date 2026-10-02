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

        public void Register(TeacherRegisterVM vm)
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
                Image = vm.Image.UploadFile(_env.WebRootPath, "images/teacher")
            };
        }
    }
}
