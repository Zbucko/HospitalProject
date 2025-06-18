using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebProjekat.Models;
using System.Globalization;

namespace WebProjekat.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin

        private string jsonPacijenti = @"C:\Users\lukaz\Desktop\Faks\WEB\webprojekat\WebProjekat\App_Data\pacijenti.json";
        private string jsonLekari = @"C:\Users\lukaz\Desktop\Faks\WEB\webprojekat\WebProjekat\App_Data\doktori.json";
        public ActionResult Index()
        {
            UcitajPacijente();
            return View();
        }

        private void UcitajPacijente()
        {

            List<Pacijent> pacijenti = JsonFileHelper.ReadJsonFile<Pacijent>(jsonPacijenti);
            ViewBag.pacijenti = pacijenti;
        }

        [HttpPost]
        public ActionResult Obrisati(string korisnickoIme)
        {
            List<Pacijent> loginDetalji = (List<Pacijent>)HttpContext.Application["pacijenti"];
            List<Pacijent> pacijenti = JsonFileHelper.ReadJsonFile<Pacijent>(jsonPacijenti);
            Pacijent p = pacijenti.Find(pa => pa.KorisnickoIme == korisnickoIme);
            if (p != null)
            {
                pacijenti.Remove(p);
                loginDetalji = pacijenti;
                JsonFileHelper.WriteJsonFile(pacijenti, jsonPacijenti);
            }


            return RedirectToAction("Index", "Admin");
        }

        [HttpPost]
        public ActionResult Azurirati(string korisnickoIme, string sifra, DateTime date, string email)
        {
            List<Pacijent> loginDetalji = (List<Pacijent>)HttpContext.Application["pacijenti"];
            List<Pacijent> pacijenti = JsonFileHelper.ReadJsonFile<Pacijent>(jsonPacijenti);
            

            Pacijent p = pacijenti.Find(pa => pa.KorisnickoIme == korisnickoIme);
            if (sifra == null || sifra == string.Empty)
            {
                sifra = p.Sifra;
            }
            if(date == null)
            {
                date = p.DatumRodjenja;
            }
            Pacijent provera = pacijenti.Find(pa => pa.Email == email);
            if (p.Equals(provera))
            {
                provera = null;
            }
            if (p != null && provera == null)
            {

                p.Sifra = sifra;
                p.DatumRodjenja = date;
                p.Email = email;
                JsonFileHelper.WriteJsonFile(pacijenti, jsonPacijenti);
            }
            loginDetalji = pacijenti;
            return RedirectToAction("Index", "Admin");

        }

        [HttpPost]
        public ActionResult Kreirati(string korisnickoIme, string jmbg, string sifra, string email, DateTime date)
        {
            List<Pacijent> loginDetalji = (List<Pacijent>)HttpContext.Application["pacijenti"];
            List<Pacijent> pacijenti = JsonFileHelper.ReadJsonFile<Pacijent>(jsonPacijenti);
            long a;
           
            Pacijent provera1 = pacijenti.Find(pa => pa.KorisnickoIme == korisnickoIme);
            Pacijent provera2 = pacijenti.Find(pa => pa.Email == email);
            Pacijent provera3 = pacijenti.Find(pa => pa.Jmbg == jmbg);
            

            if (!long.TryParse(jmbg, out a))
            {
                TempData["ErrorMessage"] = "JMBG mora sadrzati samo BROJEVE!";
                return RedirectToAction("Index", "Admin");
            }
            else if (jmbg.Length != 13)
            {
                TempData["ErrorMessage"] = "JMBG mora imati tacno 13 BROJEVA!";
                return RedirectToAction("Index", "Admin");
            }
            else if (provera1 != null || provera2 != null || provera3 != null)
            {
                if (provera1 != null)
                {
                    TempData["ErrorMessage"] = "Vec postoji pacijent sa tim korisnickim imenom ";
                    return RedirectToAction("Index", "Admin");
                }
                else if (provera2 != null)
                {
                    TempData["ErrorMessage"] = "Vec postoji pacijent sa tim Emailom";
                    return RedirectToAction("Index", "Admin");
                }
                else if (provera3 != null)
                {
                    TempData["ErrorMessage"] = "Vec postoji pacijent sa tim jmbgom";
                    return RedirectToAction("Index", "Admin");
                }
            }else
            
            {
                    pacijenti.Add(new Pacijent(korisnickoIme, jmbg, sifra, date, email));
                    JsonFileHelper.WriteJsonFile(pacijenti, jsonPacijenti);
                    loginDetalji = pacijenti;
                    return RedirectToAction("Index", "Admin");
               
             }
                

           
           
                return RedirectToAction("Index", "Admin");
            
            
        }
    }
}