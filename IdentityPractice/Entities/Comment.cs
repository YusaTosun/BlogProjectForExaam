namespace IdentityPractice.Entities
{
    public class Comment
    {
        public int CommentId { get; set; }
        public int UserId { get; set; }
        public AppUser User { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
        public string Text { get; set; }
        public bool Status { get; set; }
        public bool IsDelete { get; set; }
        public DateTime Date { get;private set; } = DateTime.Now;
    }
}
