﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebProjekat.Models
{
    public class Administrator
    {
        private string korisnickoIme;
        private string sifra;
        private string ime;
        private string prezime;
        private DateTime datumRodjenja;

        public Administrator(string korisnickoIme, string sifra, string ime, string prezime, DateTime datumRodjenja)
        {
            this.korisnickoIme = korisnickoIme;
            this.sifra = sifra;
            this.ime = ime;
            this.prezime = prezime;
            this.datumRodjenja = datumRodjenja;
        }

        public string KorisnickoIme { get => korisnickoIme; set => korisnickoIme = value; }
        public string Sifra { get => sifra; set => sifra = value; }
        public string Ime { get => ime; set => ime = value; }
        public string Prezime { get => prezime; set => prezime = value; }
        public DateTime DatumRodjenja { get => datumRodjenja; set => datumRodjenja = value; }
    }
}