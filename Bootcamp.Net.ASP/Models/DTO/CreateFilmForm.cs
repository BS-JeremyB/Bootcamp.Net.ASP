using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bootcamp.Net.ASP.Models.DTO
{
    public class CreateFilmForm
    {
        public string Titre { get; set; }
        public int AnneeSortie { get; set; }
        public string Genre { get; set; }

        public int RealisateurId { get; set; }

        public List<int> ActeursIds { get; set; } = new List<int>();

    }
}
