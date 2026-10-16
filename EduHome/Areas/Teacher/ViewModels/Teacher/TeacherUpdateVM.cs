namespace EduHome.Areas.Teacher.ViewModels.Teacher
{
    public record TeacherUpdateVM
    {        
            public string? Description { get; set; }
            public string? Speciality { get; set; }
            public string? ImageName { get; set; }
            public IFormFile? Image { get; set; }
            public string? Degree { get; set; }
            public byte? ExperienceInYear { get; set; }
            public string? Faculty { get; set; }
            public string? Firstname { get; set; }
            public string? Lastname { get; set; }
        
    }
}
