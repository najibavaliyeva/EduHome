using System.ComponentModel.DataAnnotations;

namespace EduHome.ViewModels.Category
{
    public class CategoryCreateVM
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; }
    }
}
