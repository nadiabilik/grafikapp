using System;
using System.Collections.Generic;
using System.Text;

namespace grafikapp
{
    internal class WpisGrafiku
    {
        public int Id { get; set; } // Unikalny identyfikator wpisu w grafiku, czyj to wpis
        public string Data { get; set; } // Data wpisu w grafiku
        public string TypDnia { get; set; } // Typ dnia (np. "Praca", "Wolne", "Urlop", "Choroba")
        public string GodzinaRozpoczecia { get; set; } // Godzina rozpoczęcia pracy
        public string GodzinaZakonczenia { get; set; } // Godzina zakończenia pracy

        public WpisGrafiku(int id, string data, string typDnia, string godzinaRozpoczecia, string godzinaZakonczenia)
        {
            Id = id;
            Data = data;
            TypDnia = typDnia;
            GodzinaRozpoczecia = godzinaRozpoczecia;
            GodzinaZakonczenia = godzinaZakonczenia;
        }

        public override string ToString()
        {
            if (TypDnia == "Praca")
            {
                return $"{Id}: {Data} - {TypDnia} ({GodzinaRozpoczecia} - {GodzinaZakonczenia})"               ;
            }
            else
                return $"{Id}: {Data} - {TypDnia}";
        }
    }
}
