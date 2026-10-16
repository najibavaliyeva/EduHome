using EduHome.Areas.Teacher.ViewModels.Teacher;
using EduHome.Contexts;
using EduHome.Extensions;
using EduHome.Models;
using EduHome.Models.BaseModel;
using EduHome.Services.Interfaces;
using EduHome.ViewModels.Teacher;
using EduHome.ViewModels.user;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EduHome.Services.Implements
{
    public class TeacherService : ITeacherService
    {
        readonly UserManager<BaseUser> _userManager;
        readonly IWebHostEnvironment _env;

        public TeacherService(UserManager<BaseUser> userManager, IWebHostEnvironment env)
        {
            _userManager = userManager;
            _env = env;
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
        }
        public async Task RegisterUserAsync(AppUserRegisterVM vm)
        {
            var user = new AppUser
            {
                Email = vm.Email,
                Firstname = vm.Firstname,
                Lastname = vm.Lastname,
                PhoneNumber = vm.PhoneNumber,
                BirthDate = vm.Birthdate,
                UserName = vm.Username,
            };

            var result = await _userManager.CreateAsync(user, vm.Password);
            if (!result.Succeeded) throw new Exception(result.Errors.FirstOrDefault().Description);

            result = await _userManager.AddToRoleAsync(user, "Student");
            if (!result.Succeeded) throw new Exception(result.Errors.FirstOrDefault().Description);
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
    }
}
