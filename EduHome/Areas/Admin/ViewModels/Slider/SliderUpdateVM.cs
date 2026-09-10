using System.ComponentModel.DataAnnotations;

namespace EduHome.Areas.Admin.ViewModels.Slider
{
    public record SliderUpdateVM
    {     
             public string? ImageName { get; set; }
            public IFormFile? Image { get; set; }
            public string Title { get; set; }
            public string Text { get; set; }
        
    

}
}
