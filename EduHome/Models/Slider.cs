using EduHome.Models.BaseModel;
using Microsoft.CodeAnalysis.Elfie.Model;

namespace EduHome.Models
{
    public class Slider : BaseEntity
    {
       
        public string Image { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
       
    }
}
