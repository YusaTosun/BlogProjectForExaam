using Microsoft.AspNetCore.Identity;
using System.Reflection.Metadata;

namespace IdentityPractice.Entities
{
    public class AppUser : IdentityUser<int>
    {

        public byte[]? ProfilePhoto { get; set; }
        public string Gender { get; set; }
        public string? About { get; set; }
        public string? City { get; set; }
        public string? Phone { get; set; }
        public bool Status { get; set; }
        public List<Post> Posts { get; set; }
        public AppUser()
        {
            Posts = new List<Post>();
        }

    }
}
