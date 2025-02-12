using System.ComponentModel.DataAnnotations;

namespace Bootcamp.Net.ASP.Models.DTO
{
    public class CreatePersonneForm
    {
        [Required]
        [MaxLength(100)]
        public string Nom { get; set; }
        [Required]
        [MaxLength(100)]
        public string Prenom { get; set; }
    }
}
