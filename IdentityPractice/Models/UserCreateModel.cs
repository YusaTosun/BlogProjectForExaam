using System.ComponentModel.DataAnnotations;

namespace IdentityPractice.Models
{
    public class UserCreateModel
    {
        [Required(ErrorMessage ="Kullanıcı adı gereklidir")]
        public string Username { get; set; }
        [Required(ErrorMessage ="Email adresi gereklidir")]
        [EmailAddress(ErrorMessage ="Lütfen uygun formatta bir e-mail giriniz")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Parola giriniz")]
        public string Password { get; set; }
        [Required(ErrorMessage ="Parola giriniz")]
        [Compare("Password",ErrorMessage ="Parolalar eşleşmiyor")]
        public string ConfirmPassword { get; set; }
        public string Gender { get; set; }

    }
}
