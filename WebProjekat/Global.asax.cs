using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebProjekat.Models;
namespace WebProjekat
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private const string pacijentFile = @"C:\Users\lukaz\Desktop\Faks\WEB\webprojekat\WebProjekat\App_Data\pacijenti.json";
        private const string doktorFile = @"C:\Users\lukaz\Desktop\Faks\WEB\webprojekat\WebProjekat\App_Data\doktori.json";
        private const string adminFile = @"C:\Users\lukaz\Desktop\Faks\WEB\webprojekat\WebProjekat\App_Data\admini.json";
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            


            var pacijenti = JsonFileHelper.ReadJsonFile<Pacijent>(pacijentFile);
            var doktori = JsonFileHelper.ReadJsonFile<Lekar>(doktorFile);
            var admini = JsonFileHelper.ReadJsonFile<Administrator>(adminFile);

            HttpContext.Current.Application["pacijenti"] = pacijenti;
            HttpContext.Current.Application["doktori"] = doktori;
            HttpContext.Current.Application["admini"] = admini;


            List<Pacijent> pacijentiSvi = new List<Pacijent>();
            foreach(var item in pacijenti)
            {
                pacijentiSvi.Add(item);
            }
            HttpContext.Current.Application["pacijentiSvi"] = pacijentiSvi;
            

            List<Lekar> lekari = new List<Lekar>();
            foreach (var item in doktori)
            {
                lekari.Add(item);
            }
            HttpContext.Current.Application["lekari"] = lekari;

            List<Termin> termini = new List<Termin>();
            foreach(Lekar l in lekari)
            {
                for(int i = 0; i < l.Termini.Count(); i++)
                {
                    termini.Add(l.Termini[i]);
                }
            }
            HttpContext.Current.Application["termini"] = termini;
            List<Termin> slobodniTermini = new List<Termin>();
            HttpContext.Current.Application["slobodniTermini"] = slobodniTermini;
        }
    }
}
