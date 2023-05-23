using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Cysterny
{
    public class Zadanie
    {
        Bryła[] zbiorniki;
        int objętośćWody;
        static Dictionary<string, Type> klasy = new Dictionary<string, Type>();
        public void Wczytaj(TextReader tr)
        {
            int ilośćZbiorników = Convert.ToInt32(tr.ReadLine().ToString());
            string[] linia;
            string zawieszenie, bryła;
            double zawieszenieD;
            zbiorniki = new Bryła[ilośćZbiorników];
            for(int i=0; i<zbiorniki.Length; i++)
            {
                linia = tr.ReadLine().ToString().Split(' ');
                zawieszenie = linia[0];
                zawieszenieD = Convert.ToDouble(zawieszenie);
                bryła = linia[1];
                linia = linia[2..];
                Type typ = klasy[bryła];
                Type[] typyParametrów = { typeof(double), typeof(string[]) };
                ConstructorInfo konstruktor = typ.GetConstructor(typyParametrów);
                object[] argumenty = { zawieszenieD, linia };
                zbiorniki[i] = (Bryła)konstruktor.Invoke(argumenty);
            }
            objętośćWody = Convert.ToInt32(tr.ReadLine().ToString());
        }

        public double PoliczObjętośćZbiorników(double wysokość)
        {
            double objętość = 0;
            for(int i=0; i<zbiorniki.Length; i++)
            {
                objętość += zbiorniki[i].Objętość(wysokość);
            }
            return objętość;
        }

        double ObliczWysokość(double wys1, double wys2)
        {
            return (wys1 + wys2) / 2.0;
        }

        (double,double) WyznaczNajniższeINajwyższePołożenie()
        {
            double dół=40000, góra=0;
            for(int i=0; i<zbiorniki.Length; i++)
            {
                if (dół > zbiorniki[i].Zawieszenie()) dół = zbiorniki[i].Zawieszenie();
                if (góra < zbiorniki[i].NajwyższyPunkt()) góra = zbiorniki[i].NajwyższyPunkt();
            }
            return (dół, góra);
        }

        bool CzyWZbiorniku(double wynik)
        {
            int zliczanie = 0;
            for(int i=0; i<zbiorniki.Length; i++)
            {
                if (wynik < zbiorniki[i].Zawieszenie() || wynik > zbiorniki[i].NajwyższyPunkt()) zliczanie++;
            }
            if (zliczanie == zbiorniki.Length) return false;
            return true;
        }

        public double Rozwiąż()
        {
            double dół, góra, środek, objętośćWyliczona;
            bool czyKontynuować = true;
            (dół, góra) = WyznaczNajniższeINajwyższePołożenie();
            if (PoliczObjętośćZbiorników(góra) < objętośćWody) return -1;
            do
            {
                środek = ObliczWysokość(dół, góra);
                objętośćWyliczona = PoliczObjętośćZbiorników(środek);
                if (objętośćWyliczona < objętośćWody) dół = środek;
                else góra = środek;
                if (Math.Abs(objętośćWody - objętośćWyliczona)<0.01) czyKontynuować = !CzyWZbiorniku(środek);
            } while (czyKontynuować);
            return Math.Round(środek,2);
        }

        public static void PozbierajKlasy()
        {
            string[] dllPliki = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll");

            foreach (string dllPlik in dllPliki)
            {
                try
                {
                    Assembly assembly = Assembly.LoadFrom(dllPlik);
                    Type[] typy = assembly.GetTypes();

                    foreach (Type typ in typy)
                    {
                        BryłaAttribute bryłaAttribute = (BryłaAttribute)Attribute.GetCustomAttribute(typ, typeof(BryłaAttribute));
                        if (bryłaAttribute != null)
                        {
                            klasy.Add(bryłaAttribute.name, typ);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Błąd ładowania pliku DLL: " + dllPlik);
                    Console.WriteLine(ex.ToString());
                }
            }
        }
    }
}
