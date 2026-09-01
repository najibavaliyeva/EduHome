using System.ComponentModel.DataAnnotations;

namespace EduHome.ViewModels.Slider
{
    public record SliderCreateVM
    {
        public IFormFile Image { get; set; }
        [Required]
        [MinLength(5)]
        public string Title { get; set; }
        [Required]
        [MinLength(10)]
        public string Text { get; set; }
    }
}
