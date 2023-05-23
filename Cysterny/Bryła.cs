using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cysterny
{
    [AttributeUsage(AttributeTargets.Class)]
    public class BryłaAttribute : Attribute
    {
        public string name { get; }
        public BryłaAttribute(string name)
        {
            this.name = name;
        }
    }
    abstract public class Bryła
    {
        protected double zawieszenie;
        protected double najwyższyPunkt;
        public abstract double Objętość(double wysokość);
        public double Zawieszenie()
        {
            return zawieszenie;
        }
        public double NajwyższyPunkt()
        {
            return najwyższyPunkt;
        }
    }
}
