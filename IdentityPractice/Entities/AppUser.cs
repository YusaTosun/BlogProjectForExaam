using Microsoft.AspNetCore.Identity;

namespace IdentityPractice.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public byte[]? ProfilePhoto { get; set; }
        public string Gender { get; set; }

    }
}
