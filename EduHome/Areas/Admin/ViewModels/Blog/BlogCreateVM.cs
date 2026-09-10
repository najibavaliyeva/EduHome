using System.ComponentModel.DataAnnotations;

namespace EduHome.Areas.Admin.ViewModels.Blog
{
    public record BlogCreateVM
    {
        [Required]
        public IFormFile Image { get; set; }
        [Required]
        [MinLength(3)]
        public string Title { get; set; }
        [Required]
        [MinLength(10)]
        public string Text { get; set; }
        //teacher
        [Required]
        public int CategoryId { get; set; } // one terefdr
    }
}
