using EduHome.Models.BaseModel;

namespace EduHome.Models
{
    public class AppUser:BaseUser
    {
      
        public DateOnly BirthDate { get; set; }
        public string Universty {  get; set; }

    }
}
