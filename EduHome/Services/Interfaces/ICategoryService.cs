using EduHome.ViewModels.Category;

namespace EduHome.Services.Interfaces
{
    public interface ICategoryService
    {
        void Create(CategoryCreateVM vm);
        List<CategoryGetVM> GetAll();
        //Create
        //GetAll
        //Remove
        //Update
        //GetSingle
    }
}
