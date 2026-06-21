using System;
using System.Collections.Generic;

namespace grafikapp
{
    class Program
    {
        static List<Pracownik> pracownicy = new List<Pracownik>();
        static List<WpisGrafiku> grafik = new List<WpisGrafiku>();

        static void Main(string[] args)
        {
            // Wczytanie danych pracowników z pliku CSV
            pracownicy = ZapisDanych.WczytajPracownikow();
            // Wczytanie danych grafiku z pliku CSV
            grafik = ZapisDanych.WczytajGrafik();

            Console.WriteLine("\nWitaj w aplikacji do zarządzania grafikiem pracy!\n");

            bool kontynuuj = true;
            while (kontynuuj)
            {
                Console.WriteLine("\n---MENU GŁÓWNE---\n");
                Console.WriteLine("1 Zarządzaj pracownikami");
                Console.WriteLine("2 Zarządzaj grafikiem");
                Console.WriteLine("3 Raporty");
                Console.WriteLine("0 Wyjście");

                string? wybor = Console.ReadLine();

                switch (wybor)
                {
                    case "1":
                        ZarzadzaniePracownikami();
                        break;
                    case "2":
                        ZarzadzanieGrafikiem();
                        break;
                    case "3":
                        Raporty();
                        break;
                    case "0":
                        kontynuuj = false;
                        break;
                    default:
                        Console.WriteLine("Nieprawidłowy wybór. Spróbuj ponownie. Wpisz 1, 2, 3 lub 0.");
                        break;
                }
            }

            Console.WriteLine("Dziękujemy za skorzystanie z aplikacji. Do widzenia!");
        }



        //ZARZĄDZANIE PRACOWNIKAMI
        static void ZarzadzaniePracownikami()
        {
            bool kontynuuj = true;
            while (kontynuuj)
            {
                Console.WriteLine("\n---ZARZĄDZANIE PRACOWNIKAMI---\n");
                Console.WriteLine("1 Dodaj pracownika");
                Console.WriteLine("2 Wyświetl listę pracowników");
                Console.WriteLine("3 Usuń pracownika");
                Console.WriteLine("0 Powrót do menu głównego");

                string? wybor = Console.ReadLine();

                switch (wybor)
                {
                    case "1":
                        DodajPracownika();
                        break;
                    case "2":
                        WyswietlPracownikow();
                        break;
                    case "3":
                        UsunPracownika();
                        break;
                    case "0":
                        kontynuuj = false;
                        break;
                    default:
                        Console.WriteLine("Nieprawidłowy wybór. Spróbuj ponownie. Wpisz 1, 2, 3 lub 0.");
                        break;
                }
            }
        }
        static void DodajPracownika()
        {
            Console.WriteLine("\n---DODAWANIE PRACOWNIKA---\n");
            int id = 1;
            foreach (Pracownik pracownik in pracownicy)
                if (pracownik.Id >= id) id = pracownik.Id + 1;

            string? imie = null;
            while (imie == null)
            {
                Console.WriteLine("Imię: ");
                imie = Walidator.SprawdzImie(Console.ReadLine());
            }

            string? nazwisko = null;
            while (nazwisko == null)
            {
                Console.WriteLine("Nazwisko: ");
                nazwisko = Walidator.SprawdzNazwisko(Console.ReadLine());
            }

            string? stanowisko = null;
            while (stanowisko == null)
            {
                Console.WriteLine("Stanowisko: ");
                stanowisko = Walidator.SprawdzStanowisko(Console.ReadLine());
            }

            Pracownik nowyPracownik = new Pracownik(id, imie, nazwisko, stanowisko);
            pracownicy.Add(nowyPracownik);
            ZapisDanych.ZapiszPracownikow(pracownicy);
            Console.WriteLine("Pracownik dodany pomyślnie!");
        }

        static void WyswietlPracownikow()
        {
            Console.WriteLine("\n---LISTA PRACOWNIKÓW---\n");
            if (pracownicy.Count == 0)
            {
                Console.WriteLine("Brak pracowników w bazie danych.");
                return;
            }

            foreach (Pracownik pracownik in pracownicy)
            {
                Console.WriteLine(pracownik.ToString());
            }
        }

        static void UsunPracownika()
        {
            Console.WriteLine("\n---USUWANIE PRACOWNIKA---\n");
            if (pracownicy.Count == 0)
            {
                Console.WriteLine("Brak pracowników w bazie danych.");
                return;
            }

            WyswietlPracownikow();

            int id = -1;
            while (id == -1)
            {
                Console.Write("Podaj ID pracownika do usunięcia: ");
                id = Walidator.SprawdzID(Console.ReadLine());
            }

            Pracownik pracownikDoUsuniecia = pracownicy.Find(p => p.Id == id);

            if (pracownikDoUsuniecia != null)
            {
                pracownicy.Remove(pracownikDoUsuniecia);
                ZapisDanych.ZapiszPracownikow(pracownicy);
                Console.WriteLine("Pracownik usunięty pomyślnie!");
            }
            else
            {
                Console.WriteLine($"Nie znaleziono pracownika o ID {id}.");
            }
        }

        //ZARZĄDZANIE GRAFIKIEM
        static void ZarzadzanieGrafikiem()
        {
            if (pracownicy.Count == 0)
            {
                Console.WriteLine("Nie można zarządzać grafikiem, ponieważ nie ma żadnych pracowników w bazie danych. Najpierw dodaj pracowników!");
                return;
            }

            bool kontynuuj = true;
            while (kontynuuj)
            {
                Console.WriteLine("\n---ZARZĄDZANIE GRAFIKIEM---\n");
                Console.WriteLine("1 Dodaj wpis do grafiku");
                Console.WriteLine("2 Wyświetl grafik pracownika");
                Console.WriteLine("3 Usuń wpis z grafiku");
                Console.WriteLine("0 Powrót do menu głównego");

                string wybor = Console.ReadLine();

                switch (wybor)
                {
                    case "1":
                        DodajWpisDoGrafiku();
                        break;
                    case "2":
                        WyswietlGrafikPracownika();
                        break;
                    case "3":
                        UsunWpisZGrafiku();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Nieprawidłowy wybór. Spróbuj ponownie. Wpisz 1, 2, 3 lub 0.");
                        break;
                }
            }
        }

        static void DodajWpisDoGrafiku()
        {
            Console.WriteLine("\n---DODAWANIE WPISU DO GRAFIKU---\n");
            WyswietlPracownikow();

            int id = -1;
            while (id == -1)
            {
                Console.Write("Podaj ID pracownika, dla którego chcesz dodać wpis do grafiku: ");
                id = Walidator.SprawdzID(Console.ReadLine());

                if (id != -1)
                {
                    bool istnieje = false;
                    foreach (Pracownik pracownik in pracownicy)
                        if (pracownik.Id == id) { istnieje = true; break; }
                    if (!istnieje)
                    {
                        Console.WriteLine($"Nie znaleziono pracownika o ID {id}! Spróbuj ponownie.");
                        id = -1;
                    }
                }
            }

            string data = null;
            while (data == null)
            {
                Console.Write("Data (format RRRR-MM-DD): ");
                data = Walidator.NaprawDate(Console.ReadLine());
            }

            Console.WriteLine("Typy dnia: 1-Praca, 2-Wolne, 3-Urlop, 4-Choroba");
            string typDnia = null;
            string godzinaRozpoczecia = "-";
            string godzinaZakonczenia = "-";

            while (typDnia == null)
            {
                Console.Write("Wybierz (1-4): ");
                string typDniaWybor = Console.ReadLine();

                switch (typDniaWybor)
                {
                    case "1":
                        typDnia = "Praca";

                        while (godzinaRozpoczecia == "-")
                        {
                            Console.Write("Godzina rozpoczęcia pracy (format GG:MM lub GGMM) np. 08:00 lub 0800: ");
                            string g = Walidator.NaprawGodzine(Console.ReadLine());
                            if (g != null) godzinaRozpoczecia = g;
                        }

                        while (godzinaZakonczenia == "-")
                        {
                            Console.Write("Godzina zakończenia pracy (format GG:MM lub GGMM) np. 16:00 lub 1600: ");
                            string g = Walidator.NaprawGodzine(Console.ReadLine());
                            if (g != null) godzinaZakonczenia = g;

                            if (godzinaZakonczenia != "-" && string.Compare(godzinaZakonczenia, godzinaRozpoczecia) <= 0)
                            {
                                Console.WriteLine("Godzina zakończenia pracy musi być późniejsza niż godzina rozpoczęcia pracy. Spróbuj ponownie.");
                                godzinaZakonczenia = "-";
                            }
                        }
                        break;
                    case "2":
                        typDnia = "Wolne";
                        break;
                    case "3":
                        typDnia = "Urlop";
                        break;
                    case "4":
                        typDnia = "Choroba";
                        break;
                    default:
                        Console.WriteLine("Nieprawidłowy wybór. Spróbuj ponownie. Wpisz 1, 2, 3 lub 4.");
                        break;
                }
            }

            WpisGrafiku nowyWpis = new WpisGrafiku(id, data, typDnia, godzinaRozpoczecia, godzinaZakonczenia);
            grafik.Add(nowyWpis);
            ZapisDanych.ZapiszGrafik(grafik);
            Console.WriteLine("Wpis do grafiku dodany pomyślnie!");
        }

        static void WyswietlGrafikPracownika()
        {
            Console.WriteLine("\n---WYŚWIETLANIE GRAFIKU PRACOWNIKA---\n");
            if (grafik.Count == 0)
            {
                Console.WriteLine("Brak wpisów w grafiku.");
                return;
            }

            WyswietlPracownikow();

            int id = -1;
            while (id == -1)
            {
                Console.Write("Podaj ID pracownika, którego grafik chcesz wyświetlić: ");
                id = Walidator.SprawdzID(Console.ReadLine());
                if (id != -1)
                {
                    bool istnieje = false;
                    foreach (Pracownik pracownik in pracownicy)
                        if (pracownik.Id == id) { istnieje = true; break; }
                    if (!istnieje)
                    {
                        Console.WriteLine($"Nie znaleziono pracownika o ID {id}! Spróbuj ponownie.");
                        id = -1;
                    }
                }
            }

            Console.WriteLine($"\nGrafik pracownika o ID {id}:\n");
            bool maWpisy = false;
            foreach (WpisGrafiku wpis in grafik)
                if (wpis.Id == id)
                {
                    Console.WriteLine(wpis.ToString());
                    maWpisy = true;
                }

            if (!maWpisy)
                Console.WriteLine("Brak wpisów w grafiku dla tego pracownika.");
        }

        static void UsunWpisZGrafiku()
        {
            Console.WriteLine("\n---USUWANIE WPISU Z GRAFIKU---\n");
            if (grafik.Count == 0)
            {
                Console.WriteLine("Brak wpisów w grafiku.");
                return;
            }

            WyswietlPracownikow();

            int id = -1;
            while (id == -1)
            {
                Console.Write("Podaj ID pracownika, którego wpis z grafiku chcesz usunąć: ");
                id = Walidator.SprawdzID(Console.ReadLine());
                if (id != -1)
                {
                    bool istnieje = false;
                    foreach (Pracownik pracownik in pracownicy)
                        if (pracownik.Id == id) { istnieje = true; break; }
                    if (!istnieje)
                    {
                        Console.WriteLine($"Nie znaleziono pracownika o ID {id}! Spróbuj ponownie.");
                        id = -1;
                    }
                }
            }

            Console.WriteLine($"\nWpisy w grafiku dla pracownika o ID {id}:\n");
            bool maWpisy = false;
            foreach (WpisGrafiku wpis in grafik)
                if (wpis.Id == id)
                {
                    Console.WriteLine(wpis.ToString());
                    maWpisy = true;
                }
            if (!maWpisy)
            {
                Console.WriteLine("Brak wpisów w grafiku dla tego pracownika.");
                return;
            }

            string data = null;
            while (data == null)
            {
                Console.Write("Podaj datę wpisu do usunięcia (format RRRR-MM-DD): ");
                data = Walidator.NaprawDate(Console.ReadLine());
            }

            WpisGrafiku wpisDoUsuniecia = null;
            foreach (WpisGrafiku wpis in grafik)
                if (wpis.Id == id && wpis.Data == data)
                {
                    wpisDoUsuniecia = wpis;
                    break;
                }

            if (wpisDoUsuniecia == null)
            {
                Console.WriteLine($"Nie znaleziono wpisu w grafiku dla pracownika o ID {id} z datą {data}.");
                return;
            }

            grafik.Remove(wpisDoUsuniecia);
            ZapisDanych.ZapiszGrafik(grafik);
            Console.WriteLine("Wpis z grafiku usunięty pomyślnie!");
        }

        //RAPORTY
        static void Raporty()
        {
            bool kontynuuj = true;
            while (kontynuuj)
            {
                Console.WriteLine("\n---RAPORTY---\n");
                Console.WriteLine("1 Raport obecności pracownika w danym miesiącu");
                Console.WriteLine("2 Kto pracuje w danym dniu");
                Console.WriteLine("0 Powrót do menu głównego");

                string wybor = Console.ReadLine();

                switch (wybor)
                {
                    case "1":
                        RaportObecnosci();
                        break;
                    case "2":
                        KtoPracujeWDniu();
                        break;
                    case "0":
                        kontynuuj = false;
                        break;
                    default:
                        Console.WriteLine("Nieprawidłowy wybór. Spróbuj ponownie. Wpisz 1, 2 lub 0.");
                        break;
                }
            }
        }

        static void RaportObecnosci()
        {
            Console.WriteLine("\n---RAPORT OBECNOŚCI PRACOWNIKA W DANYM MIESIĄCU---\n");
            if (grafik.Count == 0)
            {
                Console.WriteLine("Brak wpisów w grafiku.");
                return;
            }

            WyswietlPracownikow();

            int id = -1;
            while (id == -1)
            {
                Console.Write("Podaj ID pracownika, którego raport obecności chcesz wygenerować: ");
                id = Walidator.SprawdzID(Console.ReadLine());
                if (id != -1)
                {
                    bool istnieje = false;
                    foreach (Pracownik pracownik in pracownicy)
                        if (pracownik.Id == id) { istnieje = true; break; }
                    if (!istnieje)
                    {
                        Console.WriteLine($"Nie znaleziono pracownika o ID {id}! Spróbuj ponownie.");
                        id = -1;
                    }
                }
            }

            string miesiac = null;
            while (miesiac == null)
            {
                Console.Write("Podaj miesiąc i rok do raportu (format RRRR-MM lub MM.RRRR): ");
                miesiac = Walidator.SprawdzMiesiac(Console.ReadLine());
            }

            string imieNazwisko = $"ID {id}";
            foreach (Pracownik pracownik in pracownicy)
                if (pracownik.Id == id)
                {
                    imieNazwisko = $"{pracownik.Imie} {pracownik.Nazwisko}";
                    break;
                }
            int dniPracy = 0, dniWolne = 0, dniUrlopu = 0, dniChoroby = 0;
            string szczegoly = "";

            foreach (WpisGrafiku wpis in grafik)
                if (wpis.Id == id && wpis.Data.StartsWith(miesiac))
                {
                    szczegoly += wpis.ToString() + "\n";
                    switch (wpis.TypDnia)
                    {
                        case "Praca":
                            dniPracy++;
                            break;
                        case "Wolne":
                            dniWolne++;
                            break;
                        case "Urlop":
                            dniUrlopu++;
                            break;
                        case "Choroba":
                            dniChoroby++;
                            break;
                    }
                }
            string raport =
                $"--- RAPORT OBECNOŚCI ---\n" +
                $"Pracownik: {imieNazwisko}\n" +
                $"Miesiąc: {miesiac}\n" +
                $"Wygenerowano: {DateTime.Now:yyyy-MM-dd HH:mm}\n" +
                $"-----------------------\n" +
                (string.IsNullOrEmpty(szczegoly)
                    ? "Brak wpisów w grafiku dla tego pracownika w podanym miesiącu.\n"
                    : szczegoly) +
                $"-----------------------\n" +
                $"Dni pracy: {dniPracy}\n" +
                $"Dni wolne: {dniWolne}\n" +
                $"Dni urlopu: {dniUrlopu}\n" +
                $"Dni choroby: {dniChoroby}\n";

            Console.WriteLine("\n" + raport);
            ZapiszRaportDoPliku(raport, $"Raport_Obecnosci_{imieNazwisko.Replace(" ", "_")}_{miesiac.Replace(".", "_")}.txt");
        }

        static void KtoPracujeWDniu()
        {
            Console.WriteLine("\n---KTO PRACUJE W DANYM DNIU---\n");
            if (grafik.Count == 0)
            {
                Console.WriteLine("Brak wpisów w grafiku.");
                return;
            }

            string data = null;
            while(data == null)
            {
                Console.Write("Podaj datę do sprawdzenia (format RRRR-MM-DD): ");
                data = Walidator.NaprawDate(Console.ReadLine());
            }

            string naglowek =
                $"--- RAPORT DZIENNY ---\n" +
                $"Data: {data}\n" +
                $"Wygenerowano: {DateTime.Now:yyyy-MM-dd HH:mm}\n" +
                $"-----------------------\n";

            string lista = "";
            bool ktosPracuje = false;

            foreach (WpisGrafiku wpis in grafik)
                if (wpis.Data == data && wpis.TypDnia == "Praca")
                    foreach (Pracownik pracownik in pracownicy)
                        if (pracownik.Id == wpis.Id)
                        {
                            lista += $"{pracownik.Imie} {pracownik.Nazwisko} - {wpis.GodzinaRozpoczecia}-{wpis.GodzinaZakonczenia}";
                            ktosPracuje = true;
                        }


            if (!ktosPracuje)
                lista = "Brak pracowników pracujących w tym dniu.";

            string raport = naglowek + lista + "\n-----------------------\n";
            Console.WriteLine("\n" + raport);
            ZapiszRaportDoPliku(raport, $"Raport_Dzienny_{data}.txt");
        }

            static void ZapiszRaportDoPliku(string raport, string nazwaPlik)
            {
                Console.WriteLine($"Czy chcesz zapisać raport do pliku {nazwaPlik}? (T/N)");
                string odpowiedz = Console.ReadLine().ToUpper();

                while (!string.IsNullOrWhiteSpace(odpowiedz) &&
                    odpowiedz.Trim().ToLower() != "t" &&
                    odpowiedz.Trim().ToLower() != "n" &&
                    odpowiedz.Trim().ToLower() != "tak" &&
                    odpowiedz.Trim().ToLower() != "nie")

                {
                    Console.WriteLine("Nie rozumiem! Wpisz T (tak) lub N (nie).");
                    odpowiedz = Console.ReadLine();
                }

                if (!Walidator.SprawdzTakNie(odpowiedz))
                    return;

                string domyslnaNazwa = $"raport_{nazwaPlik}_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
                Console.WriteLine($"Podaj nazwę pliku do zapisu raportu (domyślnie: {domyslnaNazwa}): ");
                string nowaNazwa = Walidator.SprawdzNazwePliku(Console.ReadLine(), domyslnaNazwa);

                if (!nowaNazwa.EndsWith(".txt"))
                    nowaNazwa += ".txt";
                try
                {
                    System.IO.File.WriteAllText(nowaNazwa, raport);
                    Console.WriteLine($"Raport zapisany! Plik: {nowaNazwa}");
                }

                catch
                {
                    Console.WriteLine($"Nie udało się zapisać raportu do pliku {nazwaPlik}. Sprawdź czy nazwa jest poprawna i spróbuj ponownie.");
                }
            }
        
    }
}

                                        