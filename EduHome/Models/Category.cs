using EduHome.Models.BaseModel;

namespace EduHome.Models
{
    public class Category:BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Blog> Blogs { get; set; } = new List<Blog>(); //many teref
    }
}
