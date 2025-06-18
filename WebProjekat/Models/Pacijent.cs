using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebProjekat.Models
{
    public class Pacijent
    {
        private string korisnickoIme;
        private string jmbg;
        private string sifra;
        private DateTime datumRodjenja;
        private string email;
        private List<Termin> termini;
        private List<string> prepisaneTerapije;

        public string KorisnickoIme { get => korisnickoIme; set => korisnickoIme = value; }
        public string Jmbg { get => jmbg; set => jmbg = value; }
        public string Sifra { get => sifra; set => sifra = value; }
        public DateTime DatumRodjenja { get => datumRodjenja; set => datumRodjenja = value; }
        public string Email { get => email; set => email = value; }
        public List<Termin> Termini { get => termini; set => termini = value; }
        public List<string> PrepisaneTerapije { get => prepisaneTerapije; set => prepisaneTerapije = value; }

        public Pacijent(string korisnickoIme,string jmbg, string sifra, DateTime datumRodjenja,string email,List<Termin> termini)
        {
            this.korisnickoIme = korisnickoIme;
            this.jmbg = jmbg;
            this.sifra = sifra;
            this.datumRodjenja = datumRodjenja;
            this.email = email;
            this.termini = termini;
            this.prepisaneTerapije = new List<string>();
        }

        public Pacijent(string korisnickoIme, string jmbg, string sifra, DateTime datumRodjenja, string email)
        {
            this.korisnickoIme = korisnickoIme;
            this.jmbg = jmbg;
            this.sifra = sifra;
            this.datumRodjenja = datumRodjenja;
            this.email = email;
            this.termini = new List<Termin>();
            this.prepisaneTerapije = new List<string>();
        }

        public Pacijent()
        {
            this.prepisaneTerapije = new List<string>();
        }
    }
}