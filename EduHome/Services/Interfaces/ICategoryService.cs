using EduHome.ViewModels.Category;

namespace EduHome.Services.Interfaces
{
    public interface ICategoryService
    {
        void Create(CategoryCreateVM vm);
        List<CategoryGetVM> GetAll();
        void Remove(int id);
         CategoryGetVM GetSingle(int id);  
        void Update(int id ,CategoryUpdateVM vm);
        //Create
        //GetAll
        //Remove
        //Update
        //GetSingle
    }
}
