using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebProjekat.Models;

namespace WebProjekat.Controllers
{
    public class PacijentController : Controller
    {
        // GET: Pacijent

        private const string pacijentFile = @"C:\Users\lukaz\Desktop\Faks\WEB\webprojekat\WebProjekat\App_Data\pacijenti.json";
        public ActionResult Index()
        {
            prikaziSlobodneTermine();
            prikaziTerapije();
            return View();
        }

        private void prikaziSlobodneTermine()
        {
            List<Termin> slobodniTermini = (List<Termin>)HttpContext.Application["slobodniTermini"];
            slobodniTermini.Clear();
            List<Lekar> lekari = new List<Lekar>();
            lekari = (List<Lekar>)HttpContext.Application["lekari"];
            foreach (var item in lekari)
            {
                for (int i = 0; i < item.Termini.Count(); i++)
                {
                    if (item.Termini[i].Status == Statusi.SLOBODAN)
                    {
                        slobodniTermini.Add(item.Termini[i]);
                    }
                }

            }
            ViewBag.slobodniTermini = slobodniTermini;
            
        }

        private void prikaziTerapije()
        {
            Pacijent p = (Pacijent)Session["Pacijent"];
            ViewBag.terapije = p.PrepisaneTerapije;
        }

       
        [HttpPost]
        public ActionResult Zakazati(string ID)
        {
            int id = Int32.Parse(ID);
            Pacijent p = (Pacijent)Session["Pacijent"];
            List<Lekar> lekari = (List<Lekar>)HttpContext.Application["lekari"];
            List<Pacijent> pacijenti = JsonFileHelper.ReadJsonFile<Pacijent>(pacijentFile);

            string jsonFilePacijenti = @"C:\Users\lukaz\Desktop\Faks\WEB\webprojekat\WebProjekat\App_Data\pacijenti.json";
            string jsonFileLekari = @"C:\Users\lukaz\Desktop\Faks\WEB\webprojekat\WebProjekat\App_Data\doktori.json";


            List<Pacijent> pacijentiJson = JsonFileHelper.ReadJsonFile<Pacijent>(jsonFilePacijenti);
            List<Lekar> lekariJson = JsonFileHelper.ReadJsonFile<Lekar>(jsonFileLekari);
            
           
            foreach (Lekar l in lekari)
            {
                for (int i = 0; i < l.Termini.Count(); i++)
                {
                    if (l.Termini[i].ID == id)
                    {
                        
                        l.Termini[i].Status = Statusi.ZAKAZAN;
                        Pacijent pac = pacijentiJson.Find(pa => pa.KorisnickoIme == p.KorisnickoIme);
                        if (pac != null)
                        {
                            pac.Termini.Add(l.Termini[i]);
                            JsonFileHelper.WriteJsonFile(pacijentiJson, jsonFilePacijenti);
                        }

                        Lekar le = lekariJson.Find(lek => lek.KorisnickoIme == l.KorisnickoIme);
                        if(le != null)
                        {
                            le.Termini[i].Status = Statusi.ZAKAZAN;
                            JsonFileHelper.WriteJsonFile(lekariJson, jsonFileLekari);
                        }
                        p.Termini.Add(l.Termini[i]);
                        
                        foreach (Pacijent temp in pacijenti)
                        {
                            if (temp.Equals(p))
                            {
                                temp.Termini.Add(l.Termini[i]);
                            }
                        }
                    }
                }
            }

    
           

            return RedirectToAction("Index", "Pacijent");
        }
    }
}