using Bootcamp.Net.ASP.Data;
using Bootcamp.Net.ASP.Models;
using Bootcamp.Net.ASP.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Bootcamp.Net.ASP.Controllers
{
    public class UtilisateurController : Controller
    {
        private readonly DataContext _dc;

        public UtilisateurController(DataContext dc)
        {
            _dc = dc;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(CreateUtilisateurForm utilisateur)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            Utilisateur utilisateurAdd = new Utilisateur
            {
                Nom = utilisateur.Nom,
                Prenom = utilisateur.Prenom,
                Email = utilisateur.Email,
                Password = utilisateur.Password,
            };

            _dc.Add(utilisateurAdd);
            _dc.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}
