namespace IdentityPractice.Entities
{
    public class Student
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public DateTime CreateTime { get; set; } = DateTime.Now;
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }

    }
}
