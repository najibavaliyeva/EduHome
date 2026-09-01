using System.ComponentModel.DataAnnotations;

namespace EduHome.ViewModels.Slider
{
    public class SliderUpdateVM
    {     
             public string? ImageName { get; set; }
            public IFormFile? Image { get; set; }
            public string Title { get; set; }
            public string Text { get; set; }
        
    

}
}
