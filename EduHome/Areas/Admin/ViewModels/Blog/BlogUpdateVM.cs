using System.ComponentModel.DataAnnotations;

namespace EduHome.Areas.Admin.ViewModels.Blog
{
    public record BlogUpdateVM
    {
        public IFormFile? Image { get; set; }
        
        public string? Title { get; set; }
 
        public string? Text { get; set; }
        public string? ImageName  { get; set; }
    }
}
