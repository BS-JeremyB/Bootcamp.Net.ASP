using System.Security.Cryptography.X509Certificates;

namespace Bootcamp.Net.ASP.Models
{
    public class Film
    {
        public int Id { get; set; }
        public string Titre { get; set; }
        public int AnneeSortie { get; set; }
        public string Genre { get; set; }
        public int RealisateurId { get; set; }
        public Personne Realisateur { get; set; }
        public List<FilmPersonne> Acteurs { get; set; }
    }
}
