using System;
using System.Collections.Generic;
using System.Text;

namespace grafikapp
{
    internal class Pracownik
    {
        public int Id { get; set; } // Unikalny identyfikator pracownika
        public string Imie { get; set; } // Imię pracownika
        public string Nazwisko { get; set; } // Nazwisko pracownika
        public string Stanowisko { get; set; } // Stanowisko pracy

        // Konstruktor klasy Pracownik
        public Pracownik(int id, string imie, string nazwisko, string stanowisko)
        {
            Id = id;
            Imie = imie;
            Nazwisko = nazwisko;
            Stanowisko = stanowisko;

        }

        public override string ToString()
        {
            return $"{Id}: {Imie} {Nazwisko} - {Stanowisko}";
        }
    }
}
