using Microsoft.AspNetCore.Identity;

namespace EduHome.Models.BaseModel
{
    public class BaseUser  : IdentityUser
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }   
    }
}
