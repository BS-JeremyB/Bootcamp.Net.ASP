using Microsoft.AspNetCore.Mvc;

namespace Bootcamp.Net.ASP.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {

            Dictionary<string, string> information = new Dictionary<string, string>
            {
                {"Nom", "Doe" },
                {"Prenom", "John" },
                {"Description", "Est un dev. super nul, même s'il est dans tous les programmes du monde lors des tests." },
            };

            return View(information);
        }
    }
}
