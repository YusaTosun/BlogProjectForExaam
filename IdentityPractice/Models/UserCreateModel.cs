using IdentityPractice.Entities;
using System.ComponentModel.DataAnnotations;

namespace IdentityPractice.Models
{
    public class UserCreateModel
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }
        [Required(ErrorMessage ="Email address is required")]
        [EmailAddress(ErrorMessage ="Please use an valid Mail Adress")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [Compare("Password",ErrorMessage = "Passwords are not Match")]
        public string ConfirmPassword { get; set; }
        public string Gender { get; set; }
        public byte[]? ProfilePhoto { get; set; }
        public string? About { get; set; }
        public AppUser User { get; set; }
    }
}
