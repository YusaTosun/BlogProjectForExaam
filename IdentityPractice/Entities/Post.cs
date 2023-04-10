using System.ComponentModel.DataAnnotations.Schema;

namespace IdentityPractice.Entities
{


    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int AuthorId { get; set; }
        public virtual AppUser Author { get; set; }
        public DateTime Date { get; private set; } = DateTime.Now;
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public bool Status { get; set; }
        public Post()
        {
            Comments = new List<Comment>();
        }

    }
}
