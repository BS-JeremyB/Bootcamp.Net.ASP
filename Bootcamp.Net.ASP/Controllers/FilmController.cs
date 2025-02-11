using Bootcamp.Net.ASP.Data;
using Bootcamp.Net.ASP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bootcamp.Net.ASP.Controllers
{
    public class FilmController : Controller
    {
        private readonly DataContext _dc;

        public FilmController(DataContext dc)
        {
            _dc = dc;
        }

        public IActionResult Index()
        {
            List<Film> films = _dc.Films.ToList();
            ViewData["h1"] = "Ma liste de films";
            return View(films);
        }

        public IActionResult Detail(int id)
        {

            ViewData["PrixTicket"] = 9.99m;
            ViewBag.PrixDVD = 2_500_000_000;
            Film film = _dc.Films.Include(f => f.Realisateur)
                                 .Include(f => f.Acteurs)
                                 .ThenInclude(fp => fp.Personne)
                                 .FirstOrDefault(f => f.Id == id);
            return View(film);
        }

        public IActionResult Delete(int id)
        {
            TempData["Message"] = "Succès !";

            Film film = _dc.Films.FirstOrDefault(f => f.Id == id);

            List<FilmPersonne> personnes = _dc.FilmPersonnes.Where(fp => fp.FilmId == id).ToList();

            _dc.RemoveRange(personnes);
            _dc.Remove(film);

            
            _dc.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}
