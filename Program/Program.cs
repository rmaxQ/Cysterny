using System;
using System.IO;

namespace Cysterny
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string ścieżka = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "plik.txt");
                using StreamReader sr = new(ścieżka);
                int ilośćZadań = Convert.ToInt32(sr.ReadLine().ToString());
                double wynik;
                Zadanie.PozbierajKlasy();
                while (!sr.EndOfStream)
                {
                    Zadanie zadanie = new();
                    zadanie.Wczytaj(sr);
                    wynik = zadanie.Rozwiąż();
                    if (wynik == -1) Console.WriteLine("Overflow!");
                    else Console.WriteLine(wynik);
                }
            }
            catch (Exception e)
            {
                // Let the user know what went wrong.
                Console.WriteLine("Błąd odczytu pliku:");
                Console.WriteLine(e.Message);
            }
        }
    }
}

//Algorytm bisekcji, różne cysterny na podstawie klasy bazowej bryła, obliczanie objętości wody w punkcie między dołem a górą, metoda do obliczania kawałka objętości bryły
