using Bootcamp.Net.ASP.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bootcamp.Net.ASP.Controllers
{
    public class UtilisateurController : Controller
    {
        public IActionResult Index()
        {
            Utilisateur utilisateur = new Utilisateur
            {
                Nom = "Doe",
                Prenom = "John",
                Description = "L'inconnu le plus connus",
                Age = 30,
            };

            return View(utilisateur);
        }
    }
}
