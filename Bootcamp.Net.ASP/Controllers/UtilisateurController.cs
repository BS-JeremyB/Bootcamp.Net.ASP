using Bootcamp.Net.ASP.Data;
using Bootcamp.Net.ASP.Models;
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
        public IActionResult Index(Utilisateur utilisateur)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            _dc.Add(utilisateur);
            _dc.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}
