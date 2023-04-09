namespace IdentityPractice.Entities
{
    public class Teacher
    {
        public int TeacherId { get; set; }
        public string TeacherName { get; set; }
        public DateTime CreateTime { get; set; } = DateTime.Now;
        public ICollection<Student> Students { get; set; }
    }
}
