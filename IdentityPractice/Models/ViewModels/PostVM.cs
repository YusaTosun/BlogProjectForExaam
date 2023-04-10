using IdentityPractice.Entities;

namespace IdentityPractice.Models.ViewModels
{
	public class PostVM
	{
		public Post Post { get; set; }
		public ICollection<Post> Posts { get; set; }
		public Category Category { get; set; }
		public ICollection<Category> Categories { get; set; }

		public PostVM()
		{
			Posts = new List<Post>();
		}
	}
}
