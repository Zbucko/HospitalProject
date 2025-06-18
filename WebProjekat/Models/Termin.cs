using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebProjekat.Models
{
    public enum Statusi { SLOBODAN,ZAKAZAN};
    
    public class Termin
    {
        public static int count = 0;
        private int id;
        private Lekar lekar;
        private Statusi status;
        private DateTime datumTermina;
        private string opis;

        public Termin(Lekar lekar, Statusi status, DateTime datumTermina, string opis)
        {
            ID = count;
            this.lekar = lekar;
            this.status = status;
            this.datumTermina = datumTermina;
            this.opis = opis;
            count++;
        }

        public int ID { get { return id; } set { id = value; } }
        public Lekar Lekar { get => lekar; set => lekar = value; }
        public Statusi Status { get => status; set => status = value; }
        public DateTime DatumTermina { get => datumTermina; set => datumTermina = value; }
        public string Opis { get => opis; set => opis = value; }
    }
}