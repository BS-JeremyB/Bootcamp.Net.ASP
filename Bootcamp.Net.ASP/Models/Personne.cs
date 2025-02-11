namespace Bootcamp.Net.ASP.Models
{
    public class Personne
    {
        public int Id { get; set; }
        public string Nom {  get; set; }
        public string Prenom {  get; set; }
        public List<Film> FilmReal { get; set; }
        public List<FilmPersonne> Roles { get; set; }
    }
}
