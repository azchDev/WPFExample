
using System;

namespace WPFExample
{

    public class City : Property
    {
        public City() { }
        public City(int houseSize, int tankSize, string type, int typeID) : base(houseSize, tankSize, type, typeID) { }

        public override void SpecialService()
        {
            this.SrvMsg += "| Check house connection |";
            Console.WriteLine("...connection to the house checked...");
        }
    }
}
