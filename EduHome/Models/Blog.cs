using EduHome.Models.BaseModel;

namespace EduHome.Models
{
    public class Blog : BaseEntity
    {
        public  string Image { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; } // one teref

    }
}
