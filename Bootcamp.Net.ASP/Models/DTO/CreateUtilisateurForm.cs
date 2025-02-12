using System.ComponentModel.DataAnnotations;

namespace Bootcamp.Net.ASP.Models.DTO
{
    public class CreateUtilisateurForm
    {
        [Required]
        [MaxLength(100)]
        public string Nom {  get; set; }
        [Required]
        [MaxLength(100)]
        public string Prenom { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+=!]).{8,25}$", ErrorMessage = "Le mot de passe doit contenir 1 Maj, 1 Min, 1 chiffre, 1 char spécial")]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        public string PasswordConfirm { get; set; }
    }
}
