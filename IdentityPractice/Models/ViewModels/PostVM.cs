using IdentityPractice.Entities;

namespace IdentityPractice.Models.ViewModels
{
    public class PostVM
    {
        public Post Post { get; set; }
        public ICollection<Post> Posts { get; set; }

        public PostVM()
        {
            Posts = new List<Post>();
        }
    }
}
