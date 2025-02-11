using Bootcamp.Net.ASP.Data;
using Bootcamp.Net.ASP.Models;
using Microsoft.AspNetCore.Mvc;

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
            Film film = _dc.Films.FirstOrDefault(f => f.Titre.Contains("Star Wars"));
            return View(film);
        }
    }
}
