using System;
using System.Collections.Generic;
using System.Text;

namespace grafikapp
{
    internal class ZapisDanych
    {
        // Nazwy plików CSV do przechowywania danych pracowników i grafiku
        private static string PlikPracownicy = "pracownicy.csv";
        private static string PlikGrafik = "grafik.csv";

        // Metoda do zapisywania danych pracowników do pliku CSV
        public static void ZapiszPracownikow(List<Pracownik> pracownicy)
        {
            using (StreamWriter sw = new StreamWriter(PlikPracownicy))
            {
                foreach (var pracownik in pracownicy)
                {
                    sw.WriteLine($"{pracownik.Id};{pracownik.Imie};{pracownik.Nazwisko};{pracownik.Stanowisko}");
                }
            }
        }
        public static List<Pracownik> WczytajPracownikow()
        {
            List<Pracownik> pracownicy = new List<Pracownik>();

            if (!File.Exists(PlikPracownicy))
            {
                Console.WriteLine("Brak zapisanych pracowników - zaczynamy od nowa!");
                return pracownicy; // Zwraca pustą listę, jeśli plik nie istnieje
            }

            using (StreamReader sr = new StreamReader(PlikPracownicy))
            {
                string? linia;
                while ((linia = sr.ReadLine()) != null)
                {
                    //Pomijamy linie, które są puste lub zawierają tylko białe znaki
                    if (string.IsNullOrWhiteSpace(linia))
                        continue;

                    string[] czesci = linia.Split(';');

                    // Pomijamy linie, które nie mają dokładnie 4 części
                    if (czesci.Length != 4)
                    {
                        Console.WriteLine($"Pominięto uszkodzonego pracownika - brakuje niektórych danych.");
                        continue;
                    }

                    try
                    {
                        int id = int.Parse(czesci[0]);
                        Pracownik pracownik = new Pracownik(id, czesci[1], czesci[2], czesci[3]);
                        pracownicy.Add(pracownik);
                    }
                    catch
                    {
                        // Znamy imię i nazwisko, ale nie możemy sparsować ID, więc pomijamy tę linię
                        Console.WriteLine($"Nie udało się wczytać pracownika {czesci[1]} {czesci[2]} - dane są uszkodzone.");
                    }
                }
            }
            Console.WriteLine($"Wczytano {pracownicy.Count} pracowników z pliku.");
            return pracownicy;
        }

        // Metoda do wczytywania danych grafiku z pliku CSV
        public static void ZapiszGrafik(List<WpisGrafiku> grafik)
        {
            using (var writer = new System.IO.StreamWriter(PlikGrafik))
            {
                foreach (var wpis in grafik)
                {
                    writer.WriteLine($"{wpis.Id};{wpis.Data};{wpis.TypDnia};{wpis.GodzinaRozpoczecia};{wpis.GodzinaZakonczenia}");
                }
            }
        }

        public static List<WpisGrafiku> WczytajGrafik()
        {
            List<WpisGrafiku> grafik = new List<WpisGrafiku>();
            if (!File.Exists(PlikGrafik))
            {
                Console.WriteLine("Brak zapisanych wpisów w grafiku - zaczynamy od nowa!");
                return grafik; // Zwraca pustą listę, jeśli plik nie istnieje
            }
            using (StreamReader sr = new StreamReader(PlikGrafik))
            {
                string? linia;
                while ((linia = sr.ReadLine()) != null)
                {
                    //Pomijamy linie, które są puste lub zawierają tylko białe znaki
                    if (string.IsNullOrWhiteSpace(linia))
                        continue;
                    string[] czesci = linia.Split(';');
                    // Pomijamy linie, które nie mają dokładnie 5 części
                    if (czesci.Length != 5)                    
                    {
                        Console.WriteLine($"Pominięto uszkodzony wpis w grafiku - brakuje niektórych danych.");
                        continue;
                    }
                    try
                    {
                        int id = int.Parse(czesci[0]);
                        WpisGrafiku wpis = new WpisGrafiku(id, czesci[1], czesci[2], czesci[3], czesci[4]);
                        grafik.Add(wpis);
                    }
                    catch
                    {
                        // Znamy datę i typ dnia, ale nie możemy sparsować ID, więc pomijamy tę linię
                        Console.WriteLine($"Nie udało się wczytać wpisu w grafiku z dnia {czesci[1]} - dane są uszkodzone.");
                    }
                }
            }

            Console.WriteLine($"Wczytano {grafik.Count} wpisów w grafiku z pliku.");
            return grafik;
        }
    }
}

