using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Bootcamp.Net.ASP.Models
{
    public class Utilisateur
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Ce champs est obligatoire !")]
        public string Nom {  get; set; }
        [Required(ErrorMessage = "Ce champs est obligatoire !")]
        public string Prenom { get; set; }
        [Required(ErrorMessage = "Ce champs est obligatoire !")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Ce champs est obligatoire !")]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+=!]).{8,25}$", ErrorMessage = "Le mot de passe doit contenir 1 Maj, 1 Min, 1 chiffre, 1 char spécial")]
        public string Password { get; set; }

    }
}
