using Microsoft.AspNetCore.Identity;

namespace IdentityPractice.Entities
{
    public class AppRole : IdentityRole<int>
    {

        public DateTime CreatedTime { get; set; }
      
    }
}
