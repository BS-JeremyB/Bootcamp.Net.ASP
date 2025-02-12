using Bootcamp.Net.ASP.Models.DTO;

namespace Bootcamp.Net.ASP.Models.Mappers
{
    public static class PersonneMapper
    {
        public static Personne ToPersonne(this CreatePersonneForm form)
        {
            return new Personne
            {
                Nom = form.Nom,
                Prenom = form.Prenom,
            };
        }

        public static Personne ToPersonne(this UpdatePersonneForm form)
        {
            return new Personne
            {
                Id = form.Id,
                Nom = form.Nom,
                Prenom = form.Prenom,
            };
        }


        public static UpdatePersonneForm ToUpdate(this Personne personne)
        {
            return new UpdatePersonneForm
            {
                Id = personne.Id,
                Nom = personne.Nom,
                Prenom = personne.Prenom,
            };
        }
    }
}
