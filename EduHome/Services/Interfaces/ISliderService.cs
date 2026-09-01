using EduHome.ViewModels.Slider;

namespace EduHome.Services.Interfaces
{
    public interface ISliderService
    { 
        void Create(SliderCreateVM vm );

        List<SliderGetVM> GetAll();
        void Remove(int id);
        void Update(int id ,SliderUpdateVM vm );
        SliderGetVM GetSingle(int id); 
    }
    
}
