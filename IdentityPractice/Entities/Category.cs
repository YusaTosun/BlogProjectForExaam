using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace IdentityPractice.Entities
{

    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }

        public ICollection<Post> Posts { get; set; }
        public Category()
        {
            Posts = new List<Post>();
        }
    }
}
