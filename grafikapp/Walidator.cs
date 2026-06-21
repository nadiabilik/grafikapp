using System;
using System.Collections.Generic;
using System.Text;

namespace grafikapp
{
    internal class Walidator
    {
        // TEKST imię, nazwisko, stanowisko
        public static string SprawdzTekst(string wejscie, string nazwaPolaDoWyswietlenia)
        {
            if (string.IsNullOrWhiteSpace(wejscie))
            {
                Console.WriteLine($"Nie można pozostawić pola {nazwaPolaDoWyswietlenia} pustego. Proszę wprowadzić poprawną wartość.");
                return null;
            }

            wejscie = wejscie.Trim();

            if (wejscie.Length < 2)
            {
                Console.WriteLine($"Pole {nazwaPolaDoWyswietlenia} musi zawierać co najmniej 2 znaki. Proszę wprowadzić poprawną wartość.");
                return null;
            }

            if (wejscie.Length > 50)
            {
                Console.WriteLine($"Pole {nazwaPolaDoWyswietlenia} nie może zawierać więcej niż 50 znaków. Proszę wprowadzić poprawną wartość.");
                return null;
            }

            foreach (char c in wejscie)
            {
                if (!char.IsLetter(c) && c != ' ' && c != '-')
                {
                    Console.WriteLine($"Pole {nazwaPolaDoWyswietlenia} zawiera niedozwolony znak. Pole może zawierać tylko litery, spacje i myślniki. Proszę wprowadzić poprawną wartość.");
                    return null;
                }
            }

            if (wejscie[0] == ' ' || wejscie[0] == '-' )
            {
                Console.WriteLine($"Pole {nazwaPolaDoWyswietlenia} nie może zaczynać się od spacji lub myślnika. Proszę wprowadzić poprawną wartość.");
                return null;
            }

            if (wejscie[wejscie.Length - 1] == ' ' || wejscie[wejscie.Length - 1] == '-')
            {
                Console.WriteLine($"Pole {nazwaPolaDoWyswietlenia} nie może kończyć się na spację lub myślnik. Proszę wprowadzić poprawną wartość.");
                return null;
            }

            return wejscie;
        }

        // ID
        public static int SprawdzID(string wejscie)
        {
            if (string.IsNullOrWhiteSpace(wejscie))
            {
                Console.WriteLine("Nie można pozostawić pola ID pustego. Proszę wprowadzić poprawną wartość.");
                return -1;
            }

            wejscie = wejscie.Trim();

            foreach (char c in wejscie)
            {
                if (!char.IsDigit(c))
                {
                    Console.WriteLine("Pole ID może zawierać tylko cyfry. Proszę wprowadzić poprawną wartość.");
                    return -1;
                }
            }

            int wynik;
            if (!int.TryParse(wejscie, out wynik))
            {
                Console.WriteLine("Wprowadzona wartość ID jest zbyt duża. Proszę wprowadzić poprawną wartość.");
                return -1;
            }

            if (wynik <= 0)
            {
                Console.WriteLine("Pole ID musi być większe od 0. Proszę wprowadzić poprawną wartość.");
                return -1;
            }

            if (wynik > 999999)
            {
                Console.WriteLine("Pole ID nie może być większe niż 999999. Proszę wprowadzić poprawną wartość.");
                return -1;
            }

            return wynik;
        }

        // DATA
        public static string NaprawDate(string wejscie)
        {
            if (string.IsNullOrWhiteSpace(wejscie))
            {
                Console.WriteLine("Nie można pozostawić pola Data pustego. Proszę wprowadzić poprawną wartość. Wpisz datę w formacie: RRRR-MM-DD lub DD.MM.RRRR.");
                return null;
            }

            wejscie = wejscie.Trim();

            foreach (char c in wejscie)
            {
                if (!char.IsDigit(c) && c != '-' && c != '.' && c != ' ')
                {
                    Console.WriteLine("Pole Data zawiera niedozwolony znak. Pole może zawierać tylko cyfry, myślniki, kropki i spacje. Proszę wprowadzić poprawną wartość. Wpisz datę w formacie: RRRR-MM-DD lub DD.MM.RRRR.");
                    return null;
                }
            }

            string[] formaty =
            {
                "yyyy-MM-dd",
                "dd.MM.yyyy",
                "yyyy/MM/dd",
                "dd/MM/yyyy",
                "yyyy MM dd",
                "dd MM yyyy",
                "dd-MM-yyyy",
                "yyyy.MM.dd",
                "d.M.yyyy"
            };

            foreach (string format in formaty)
            {
                DateTime wynik;
                if (DateTime.TryParseExact(wejscie, format,
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.None,
                    out wynik))
                {
                    return wynik.ToString("yyyy-MM-dd");
                }
            }

            Console.WriteLine("Niepoprawna data. Proszę wprowadzić poprawną wartość. Wpisz datę w formacie: RRRR-MM-DD lub DD.MM.RRRR.");
            return null;
        }

        // GODZINA
        public static string NaprawGodzine(string wejscie)
        {
            if (string.IsNullOrWhiteSpace(wejscie))
            {
                Console.WriteLine("Nie można pozostawić pola Godzina pustego. Proszę wprowadzić poprawną wartość. Wpisz godzinę w formacie: GG:MM.");
                return null;
            }

            wejscie = wejscie.Trim().Replace(".", ":").Replace(",", ":");

            foreach (char c in wejscie)
            {
                if (char.IsLetter(c))
                {
                    Console.WriteLine("Pole Godzina zawiera niedozwolony znak. Pole może zawierać tylko cyfry i dwukropki. Proszę wprowadzić poprawną wartość. Wpisz godzinę w formacie: GG:MM.");
                    return null;
                }
            }

            int godzina, minuta;

            if (wejscie.Contains(":"))
            {
                string[] czesci = wejscie.Split(':');
                if (czesci.Length != 2)
                {
                    Console.WriteLine("Niepoprawny format godziny. Za dużo dwukropków. Proszę wprowadzić poprawną wartość. Wpisz godzinę w formacie: GG:MM.");
                    return null;
                }

                if (!int.TryParse(czesci[0], out godzina) || !int.TryParse(czesci[1], out minuta))
                {
                    Console.WriteLine("Niepoprawny format godziny. Proszę wprowadzić poprawną wartość. Godzina musi zawierać tylko cyfry. Wpisz godzinę w formacie: GG:MM.");
                    return null;
                }
            }

            else if (int.TryParse(wejscie, out int liczba))
            {
                if (wejscie.Length <= 2)
                {
                    godzina = liczba;
                    minuta = 0;
                }

                else if (wejscie.Length == 3)
                {
                    godzina = int.Parse(wejscie.Substring(0, 1));
                    minuta = int.Parse(wejscie.Substring(1, 2));
                }

                else if (wejscie.Length == 4)
                {
                    godzina = int.Parse(wejscie.Substring(0, 2));
                    minuta = int.Parse(wejscie.Substring(2, 2));
                }

                else
                {
                    Console.WriteLine("Niepoprawny format godziny. Za dużo cyfr. Proszę wprowadzić poprawną wartość. Wpisz godzinę w formacie: GG:MM.");
                    return null;
                }
            }

            else
            {
                Console.WriteLine("Niepoprawny format godziny. Proszę wprowadzić poprawną wartość. Wpisz godzinę w formacie: GG:MM.");
                return null;
            }

            if (godzina < 0 || godzina > 23 || minuta < 0 || minuta > 59)
            {
                Console.WriteLine("Niepoprawny format godziny. Godzina musi być w zakresie 00-23, a minuta w zakresie 00-59. Proszę wprowadzić poprawną wartość. Wpisz godzinę w formacie: GG:MM.");
                return null;
            }

            return $"{godzina:D2}:{minuta:D2}";
        }

        //MIESIĄC
        public static string SprawdzMiesiac(string wejscie)
        {
            if (string.IsNullOrWhiteSpace(wejscie))
            {
                Console.WriteLine("Nie można pozostawić pola Miesiąc pustego. Wpisz np. 2026-06 lub 06.2026");
                return null;
            }

            wejscie = wejscie.Trim();
            
            if (wejscie.Contains(".") && !wejscie.Contains("-"))
            {
                string[] czesci = wejscie.Split('.');
                if (czesci.Length == 2)
                    wejscie = $"{czesci[1]}-{czesci[0]}";
            }

            if (wejscie.Length != 7 || wejscie[4] != '-')
            {
                Console.WriteLine("Niepoprawny format miesiąca. Wpisz np. 2026-06 lub 06.2026");
                return null;
            }

            int rok, miesiac;
            if (!int.TryParse(wejscie.Substring(0, 4), out rok) ||
                !int.TryParse(wejscie.Substring(5, 2), out miesiac))
            {
                Console.WriteLine("Niepoprawny format miesiąca. Wpisz np. 2026-06 lub 06.2026");
                return null;
            }

            if (rok < 2000 || rok > 2100)
            {
                Console.WriteLine($"Rok '{rok}' jest poza zakresem 2000-2100.");
                return null;
            }

            if (miesiac < 1 || miesiac > 12)
            {
                Console.WriteLine($"Miesiąc '{miesiac}' musi być w zakresie 1-12.");
                return null;
            }

            return wejscie;
        }

        //ODPOWIEDZ TAK/NIE
        public static bool SprawdzTakNie(string wejscie)
        {
            if (string.IsNullOrWhiteSpace(wejscie))
                return false;

            wejscie = wejscie.Trim().ToLower();

            if (wejscie == "tak" || wejscie == "t" || wejscie == "y" || wejscie == "yes" || wejscie == "1")
                return true;
            if (wejscie == "nie" || wejscie == "n" || wejscie == "no")
                return false;

            Console.WriteLine("Niepoprawna odpowiedź. Proszę wprowadzić tak lub nie.");
            return false;
        }

        //NAZWA PLIKU
        public static string SprawdzNazwePliku(string wejscie, string domyslna)
        {
            if (string.IsNullOrWhiteSpace(wejscie))
                return domyslna;

            wejscie = wejscie.Trim();

            char[] niedozwoloneZnaki = { '\\', '/', ':', '*', '?', '"', '<', '>', '|' };
            foreach (char c in niedozwoloneZnaki)
            {
                if (wejscie.Contains(c))
                {
                    Console.WriteLine($"Nazwa pliku nie może zawierać znaków: {new string(niedozwoloneZnaki)}. Została użyta domyślna nazwa.");
                    return domyslna;
                }
            }

            if (wejscie.Length > 100)
            {
                Console.WriteLine("Nazwa pliku nie może być dłuższa niż 100 znaków. Została użyta domyślna nazwa.");
                return domyslna;
            }

            return wejscie;
        }
    }
}

