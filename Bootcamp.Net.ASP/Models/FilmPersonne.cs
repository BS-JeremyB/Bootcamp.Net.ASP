namespace Bootcamp.Net.ASP.Models
{
    public class FilmPersonne
    {
        public int FilmId { get; set; }
        public Film Film { get; set; }

        public int PersonneId { get; set; }
        public Personne Personne { get; set; }
    }
}
