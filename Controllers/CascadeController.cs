using CascadeDropdwon.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CascadeDropdwon.Controllers
{
    public class CascadeController : Controller
    {
        private readonly ApplicationContext context;

        public CascadeController(ApplicationContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            var Countries = context.Countries.ToList();
            return View(Countries);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Create(Country country)
        {
            context.Countries.Add(country);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult StateIndex()
        {
            var State = context.States.ToList();
            return View(State);
        }
        public IActionResult StateCreate()
        {
            ViewBag.Country = new SelectList(context.Countries, "Id", "Name");
            return View();
        }
        [HttpPost]
        public IActionResult StateCreate(State state)
        {
            context.States.Add(state);
            context.SaveChanges();
            return RedirectToAction("StateIndex");
        }
        public IActionResult CityIndex()
        {
            var City = context.Cities.ToList();
            return View(City);
        }
        public IActionResult CityCreate()
        {
            ViewBag.State = new SelectList(context.States, "Id", "Name");
            return View();
        }
        [HttpPost]
        public IActionResult CityCreate(City city)
        {
            context.Cities.Add(city);
            context.SaveChanges();
            return RedirectToAction("CityIndex");
        }
        public IActionResult CascadeList()
        {
            ViewBag.Countries = new SelectList(context.Countries, "Id", "Name");
            return View();
        }
        public IActionResult LoadState(int Id)
        {
            var State = context.States.Where(z => z.CountryId == Id).ToList();
            return Json(new SelectList(State, "Id", "Name"));
        }
        public IActionResult LoadCity(int Id)
        {
            var City = context.Cities.Where(z => z.StateId == Id).ToList();
            return Json(new SelectList(City, "Id", "Name"));
        }
        public IActionResult DropDown()
        {
            return View();
        }
    }
}
