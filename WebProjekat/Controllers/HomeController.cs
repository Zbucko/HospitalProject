using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebProjekat.Models;
using System.IO;

namespace WebProjekat.Controllers
{
    public class HomeController : Controller
    {
        
        private const string pacijentFile = @"C:\Users\lukaz\Desktop\Faks\WEB\webprojekat\WebProjekat\App_Data\pacijenti.json";

        public ActionResult Index()
        {
            ViewBag.ErrorMessage = null;
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username,string password)
        {


            List<Pacijent> pacijenti = JsonFileHelper.ReadJsonFile<Pacijent>(pacijentFile);
            List<Lekar> doktori = (List<Lekar>)HttpContext.Application["doktori"];
            List<Administrator> admini = (List<Administrator>)HttpContext.Application["admini"];

            var pacijent = pacijenti.FirstOrDefault(p => p.KorisnickoIme == username && p.Sifra == password);
            var lekar = doktori.FirstOrDefault(p => p.KorisnickoIme == username && p.Sifra == password);
            var admin = admini.FirstOrDefault(p => p.KorisnickoIme == username && p.Sifra == password);

            if(pacijent != null)
            {
                Session["Pacijent"] = pacijent;
               
                return RedirectToAction("Index", "Pacijent");
            }
            else if(lekar != null)
            {
                Session["Lekar"] = lekar;
                
                return RedirectToAction("Index", "Lekar");
            }
            else if(admin != null)
            {
                Session["Admin"] = admin;
               
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                TempData["ErrorMessage"] = "Pogresno korisnicko ime ili lozinka!";
                
                return RedirectToAction("Index","Home");
            }
        }
    }
}