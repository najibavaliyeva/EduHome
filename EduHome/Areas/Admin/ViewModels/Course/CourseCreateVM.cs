using EduHome.Enums;

namespace EduHome.Areas.Admin.ViewModels.Course
{
    public class CourseCreateVM
    {
        public IFormFile Image { get; set; }
        public string Title { get; set; }
        public string Info { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateOnly StartsAt { get; set; }
        public byte DurationInMonth { get; set; }
        public byte ClassDurationInHours { get; set; }
        public SkillLevel SkillLevel { get; set; }
        public Language Language { get; set; }
        public ushort StudentCapacity { get; set; }
        public bool IsSelfAssesment { get; set; }
        public int CategoryId { get; set; }
    }
}
