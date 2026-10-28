namespace EduHome.ViewModels.Student
{
    public record StudentGetVM
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string University { get; set; }
        public string Email { get; set; }
        public DateOnly BirthDate { get; set; }
    }
}