using EduHome.Areas.Admin.ViewModels.Blog;

namespace EduHome.Services.Interfaces
{
    public interface IBlogService
    {
        //create
        Task CreateAsync( BlogCreateVM vm);

        //remove
        Task RemoveAsync(int id);
        //update
        Task UpdateAsync( int id, BlogUpdateVM vm);
        //getall

         Task <List<BlogGetVM> > GetAllAsync();
        //getsingle
         Task <BlogGetVM > GetSingleAsync(int id);
    }
}
