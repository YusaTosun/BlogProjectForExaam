//using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace IdentityPractice.Models
{
    public class UserSignInModel
    {
        [Required(ErrorMessage="Kullanıcı adı gerekli")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Parola gerekli")]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
}
