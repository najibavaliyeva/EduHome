using EduHome.Enums;

namespace EduHome.Areas.Admin.ViewModels.Course
{
    public class CourseGetVM
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public string Info { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateOnly StartsAt { get; set; }
        public byte DurationInMonth { get; set; }
        public byte ClassDurationInHours { get; set; }
        public string SkillLevel { get; set; } 
        public SkillLevel Level { get; set; }
        public string Language { get; set; }
        public Language LanguageValue { get; set; } 
        public ushort StudentCapacity { get; set; }
        public bool IsSelfAssesment { get; set; }
        public int CategoryId { get; set; }
    }
}
