using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebProjekat.Models
{
    public class Lekar
    {
        private string korisnickoIme;
        private string sifra;
        private string ime;
        private string prezime;
        private DateTime datumRodjenja;
        private string email;
        private List<Termin> termini;

        public Lekar(string korisnickoIme, string sifra, string ime, string prezime, DateTime datumRodjenja, string email, List<Termin> termini)
        {
            this.korisnickoIme = korisnickoIme;
            this.sifra = sifra;
            this.ime = ime;
            this.prezime = prezime;
            this.datumRodjenja = datumRodjenja;
            this.email = email;
            this.termini = termini;
        }

        public string KorisnickoIme { get => korisnickoIme; set => korisnickoIme = value; }
        public string Sifra { get => sifra; set => sifra = value; }
        public string Ime { get => ime; set => ime = value; }
        public string Prezime { get => prezime; set => prezime = value; }
        public DateTime DatumRodjenja { get => datumRodjenja; set => datumRodjenja = value; }
        public string Email { get => email; set => email = value; }
        public List<Termin> Termini { get => termini; set => termini = value; }
    }
}