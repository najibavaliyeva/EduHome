using Microsoft.AspNetCore.Identity;

namespace EduHome.Models
{
    public class Role : IdentityRole
    {
        public string Description { get; set; }
    }
}
