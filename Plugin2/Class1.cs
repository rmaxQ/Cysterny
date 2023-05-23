using System;

namespace Cysterny
{
    [Bryła("s")]
    class Stożek : Bryła
    {
        double wysokość, promień;

        public Stożek(double zawieszenie, string[] tekst)
        {
            this.zawieszenie = zawieszenie;
            wysokość = Convert.ToDouble(tekst[0]);
            promień = Convert.ToDouble(tekst[1]);
            najwyższyPunkt = zawieszenie + wysokość;
        }

        public override double Objętość(double wysokość)
        {
            double wysokośćObcięcia = wysokość - zawieszenie;
            if (wysokośćObcięcia <= 0) return 0;
            if (wysokość <= wysokośćObcięcia)
            {
                return 1.0 / 3.0 * Math.PI * Math.Pow(promień, 2) * wysokość;
            }
            double wysokośćStożkaPomocniczego = wysokość - wysokośćObcięcia;
            double promieńStożkaPomocniczego = wysokośćStożkaPomocniczego * promień / wysokość;
            return 1.0 / 3.0 * Math.PI * Math.Pow(promień, 2) * wysokość - (1.0 / 3.0 * Math.PI * Math.Pow(promieńStożkaPomocniczego, 2) * wysokośćStożkaPomocniczego);
        }
    }

    [Bryła("w")]
    class Walec : Bryła
    {
        double wysokość, promień;
        double poleKoła;

        public Walec(double zawieszenie, string[] tekst)
        {
            this.zawieszenie = zawieszenie;
            wysokość = Convert.ToDouble(tekst[0]);
            promień = Convert.ToDouble(tekst[1]);
            najwyższyPunkt = zawieszenie + wysokość;
            poleKoła = Math.Pow(promień, 2) * Math.PI;
        }

        public override double Objętość(double wysokość)
        {
            double wysokośćObcięcia = wysokość - zawieszenie;
            if (wysokośćObcięcia <= 0) return 0;
            if (wysokość <= wysokośćObcięcia)
            {
                return poleKoła * wysokość;
            }
            return poleKoła * wysokośćObcięcia;
        }
    }
}
