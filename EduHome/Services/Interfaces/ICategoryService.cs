using EduHome.Areas.Admin.ViewModels.Category;

namespace EduHome.Services.Interfaces
{
    public interface ICategoryService
    {
        Task CreateAsync(CategoryCreateVM vm);
        Task <List<CategoryGetVM>> GetAllAsync();
        Task RemoveAsync(int id);
        Task  <CategoryGetVM> GetSingleAsync(int id);  
        Task UpdateAsync(int id ,CategoryUpdateVM vm);
        //CreateAsync
        //GetAllAsync
        //RemoveAsync
        //Update
        //GetSingle
    }
}
