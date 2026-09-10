using EduHome.Areas.Admin.ViewModels.Blog;

namespace EduHome.Services.Interfaces
{
    public interface IBlogService
    {
        //create
        void Create( BlogCreateVM vm);

        //remove
        void Remove(int id);
        //update
        void Update( int id, BlogUpdateVM vm);
        //getall

        List<BlogGetVM> GetAll();
        //getsingle
        BlogGetVM GetSingle(int id);
    }
}
