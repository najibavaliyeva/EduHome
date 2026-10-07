using EduHome.Areas.Admin.ViewModels.Slider;

namespace EduHome.Services.Interfaces
{
    public interface ISliderService
    { 
        Task CreateAsync(SliderCreateVM vm );

         Task <List<SliderGetVM>> GetAllAsync();
        Task RemoveAsync(int id);
        Task UpdateAsync(int id ,SliderUpdateVM vm );
        Task <SliderGetVM> GetSingleAsync(int id); 
    }
    
}
