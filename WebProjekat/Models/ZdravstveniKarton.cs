using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebProjekat.Models
{
    public class ZdravstveniKarton
    {
        private List<Termin> termini;
        private Pacijent pacijent;

        public ZdravstveniKarton(List<Termin> termini, Pacijent pacijent)
        {
            this.termini = termini;
            this.pacijent = pacijent;
        }

        public List<Termin> Termini { get => termini; set => termini = value; }
        public Pacijent Pacijent { get => pacijent; set => pacijent = value; }
    }
}