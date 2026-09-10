namespace EduHome.Areas.Admin.ViewModels.Slider
{
    public record SliderGetVM
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
