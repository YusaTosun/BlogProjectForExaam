//using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace IdentityPractice.Models
{
    public class UserSignInModel
    {
        [Required(ErrorMessage= "Username is Required")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is Required")]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }
}
