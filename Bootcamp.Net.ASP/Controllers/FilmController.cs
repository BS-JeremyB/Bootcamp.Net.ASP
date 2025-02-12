using Bootcamp.Net.ASP.Data;
using Bootcamp.Net.ASP.Models;
using Bootcamp.Net.ASP.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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


        public IActionResult Create()
        {
            ViewBag.Personnes = _dc.Personnes.Select(p => new SelectListItem { Value = p.Id.ToString(), Text = $"{p.Prenom} {p.Nom}" }).ToList();
            //CreateFilmForm createFilm = new CreateFilmForm
            //{
            //    Realisateurs = _dc.Personnes.Select(p => new SelectListItem { Value = p.Id.ToString(), Text = $"{p.Prenom} {p.Nom}" }).ToList(),
            //    Acteurs = _dc.Personnes.Select(p => new SelectListItem { Value = p.Id.ToString(), Text = $"{p.Prenom} {p.Nom}" }).ToList()
            //};
        
            return View();
        }


        [HttpPost]
        public IActionResult Create(CreateFilmForm form)
        {
            if (!ModelState.IsValid)
            {
                return View(form);
            }

            Film filmAdd = new Film
            {
                Titre = form.Titre,
                AnneeSortie = form.AnneeSortie,
                Genre = form.Genre,
                RealisateurId = form.RealisateurId,
            };

            _dc.Films.Add(filmAdd);
            _dc.SaveChanges();

            if(form.ActeursIds != null && form.ActeursIds.Count > 0)
            {
                var filmPersonnes = form.ActeursIds.Select(id => new FilmPersonne { FilmId = filmAdd.Id, PersonneId = id }).ToList();

                _dc.FilmPersonnes.AddRange(filmPersonnes);
                _dc.SaveChanges();
            }

            return RedirectToAction("Index");

        }
        



    }
}
