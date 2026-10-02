

using EduHome.Areas.Admin.ViewModels.Role;

namespace EduHome.Services.Interfaces
{
    public interface IRoleService
    {

        Task<List<RoleGetVM>> GetAllAsync();
        Task CreateAsync( RoleCreateVM vm);
        Task RemoveAsync(string id);
    }
}
