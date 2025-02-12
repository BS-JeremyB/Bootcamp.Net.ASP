using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bootcamp.Net.ASP.Data;
using Bootcamp.Net.ASP.Models;
using Bootcamp.Net.ASP.Models.DTO;
using Bootcamp.Net.ASP.Models.Mappers;

namespace Bootcamp.Net.ASP.Controllers
{
    public class PersonnesController : Controller
    {
        private readonly DataContext _dc;

        public PersonnesController(DataContext dc)
        {
            _dc = dc;
        }

        // GET: Personnes
        public IActionResult  Index()
        {
            return View(_dc.Personnes.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Create(CreatePersonneForm personne)
        {

            if(!ModelState.IsValid)
            {
                return View();
            }

            _dc.Add(personne.ToPersonne());
            _dc.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            Personne? personne = _dc.Personnes.FirstOrDefault(p => p.Id == id);

            if (personne == null)
            {
                RedirectToAction("Index");
            }

            return View(personne.ToUpdate()); 
        }

        [HttpPost]
        public IActionResult Update(UpdatePersonneForm personne) {



            if (!ModelState.IsValid)
            {
                return View();
            }

            _dc.Update(personne.ToPersonne());
            _dc.SaveChanges();

            return RedirectToAction("Index");

        }


    }
}
