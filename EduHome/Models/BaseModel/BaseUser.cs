using Microsoft.AspNetCore.Identity;

namespace EduHome.Models.BaseModel
{
    public class BaseUser  : IdentityUser
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
       public DateTime CreatedAt { get; set; }
       public DateTime? UpdatedAt { get; set; }   
    }
}
