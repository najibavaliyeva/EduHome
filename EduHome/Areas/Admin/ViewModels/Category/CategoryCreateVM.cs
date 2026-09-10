using System.ComponentModel.DataAnnotations;

namespace EduHome.Areas.Admin.ViewModels.Category
{
    public record CategoryCreateVM
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; }
    }
}
