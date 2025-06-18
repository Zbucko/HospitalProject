using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebProjekat.Models;

namespace WebProjekat.Controllers
{
    public class LekarController : Controller
    {


        // GET: Lekar

        private const string pacijentFile = @"C:\Users\lukaz\Desktop\Faks\WEB\webprojekat\WebProjekat\App_Data\pacijenti.json";
        public ActionResult Index()
        {
            Lekar l = (Lekar)Session["Lekar"];
            PopuniTermine(l);
            
            PopuniTerapije(l);
            return View();
        }


        
        private void PopuniTermine(Lekar l) 
        {
            List<Termin> terminiLekara = new List<Termin>();
            foreach(Termin t in l.Termini)
            {
                terminiLekara.Add(t);
            }

            ViewBag.termini = terminiLekara;
        }

        private void PopuniTerapije(Lekar l)
        {
            List<Pacijent> pacijenti = JsonFileHelper.ReadJsonFile<Pacijent>(pacijentFile);
            List<Pacijent> terapije = new List<Pacijent>();
            
           foreach (Pacijent p in pacijenti)
            {
                for (int i = 0; i < p.Termini.Count(); i++)
                {
                    if (l.KorisnickoIme == p.Termini[i].Lekar.KorisnickoIme)
                    {
                        if (!terapije.Contains(p))
                        {
                            terapije.Add(p);
                            JsonFileHelper.WriteJsonFile(pacijenti, pacijentFile);
                        }
                    } 
                }

            }
            
            ViewBag.terapije = terapije;
        }

        public ActionResult PrepisatiTerapiju(string korisnickoIme, string terapija)
        {
            string pacijentiPath = @"C:\Users\lukaz\Desktop\Faks\WEB\webprojekat\WebProjekat\App_Data\pacijenti.json";
            List<Pacijent> pacijenti = (List<Pacijent>)HttpContext.Application["pacijentiSvi"];
            List<Pacijent> pacijentiJson = JsonFileHelper.ReadJsonFile<Pacijent>(pacijentiPath);
            Pacijent temp = pacijentiJson.Find(pa => pa.KorisnickoIme == korisnickoIme);
            if (temp != null)
            {
                temp.PrepisaneTerapije.Add(terapija);
                JsonFileHelper.WriteJsonFile<Pacijent>(pacijentiJson, pacijentiPath);
            }
            foreach(Pacijent p in pacijenti)
            {
                if(p.KorisnickoIme == korisnickoIme)
                {
                    p.PrepisaneTerapije.Add(terapija);
                }
            }
            return RedirectToAction("Index", "Lekar");
        }

        public ActionResult KreirajTermin(DateTime date,string opis)
        {
            string lekariPath = @"C:\Users\lukaz\Desktop\Faks\WEB\webprojekat\WebProjekat\App_Data\doktori.json";
            Lekar l = (Lekar)Session["Lekar"];
            Termin t = new Termin(l, Statusi.SLOBODAN, date, opis);
           
            List<Lekar> lekariJson = JsonFileHelper.ReadJsonFile<Lekar>(lekariPath);
            Lekar temp = lekariJson.Find(te => te.KorisnickoIme == l.KorisnickoIme);
            if (temp != null)
            {
                temp.Termini.Add(t);
                JsonFileHelper.WriteJsonFile<Lekar>(lekariJson, lekariPath);
            }
            l.Termini.Add(t);
            return RedirectToAction("Index", "Lekar");
        }
        
    }
}