using System;

namespace Cysterny
{
    [Bryła("k")]
    class Kula : Bryła
    {
        double promień;
        public Kula(double zawieszenie, string[] tekst)
        {
            double promień = Convert.ToDouble(tekst[0]);
            this.zawieszenie = zawieszenie;
            this.promień = promień;
            najwyższyPunkt = zawieszenie + (promień * 2);
        }
        public override double Objętość(double wysokość)
        {
            double wysokośćObcięcia = wysokość - zawieszenie;
            if (wysokośćObcięcia <= 0) return 0;
            bool czyPoniżejŚrodka = true;
            if (promień * 2 <= wysokośćObcięcia)
            {
                return 4.0 / 3.0 * Math.PI * Math.Pow(this.promień, 3);
            }
            if (wysokośćObcięcia > promień)
            {
                wysokośćObcięcia = promień * 2 - wysokośćObcięcia;
                czyPoniżejŚrodka = false;
            }
            double objętośćOdcinka = (Math.Pow(wysokośćObcięcia, 2) / 3.0 * Math.PI * (3 * promień - wysokośćObcięcia));
            return czyPoniżejŚrodka ? objętośćOdcinka : 4.0 / 3.0 * Math.PI * Math.Pow(this.promień, 3) - objętośćOdcinka;
        }
    }

    [Bryła("p")]
    class Prostopadłościan : Bryła
    {
        double wysokość, długość, szerokość;

        public Prostopadłościan(double zawieszenie, string[] tekst)
        {
            this.zawieszenie = zawieszenie;
            wysokość = Convert.ToDouble(tekst[0]);
            długość = Convert.ToDouble(tekst[1]);
            szerokość = Convert.ToDouble(tekst[2]);
            najwyższyPunkt = zawieszenie + wysokość;
        }

        public override double Objętość(double wysokość)
        {
            double wysokośćObcięcia = wysokość - zawieszenie;
            if (wysokośćObcięcia <= 0) return 0;
            if (wysokość <= wysokośćObcięcia)
            {
                return wysokość * szerokość * długość;
            }
            return wysokośćObcięcia * szerokość * długość;
        }
    }
}
