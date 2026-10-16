namespace EduHome.ViewModels.Teacher
{
    public record TeacherGetAllVM
    {
        public string Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Speciality { get; set; }
        public string Image { get; set; }
    }
}