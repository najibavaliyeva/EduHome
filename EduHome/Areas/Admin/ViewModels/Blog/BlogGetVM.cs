namespace EduHome.Areas.Admin.ViewModels.Blog
{
    public record BlogGetVM
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string CategoryName { get; set; }
    }
}
