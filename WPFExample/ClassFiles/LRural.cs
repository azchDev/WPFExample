
using System;

namespace WPFExample
{

    public class LRural : Property
    {

        public LRural() { }
        public LRural(int houseSize, int tankSize, string type, int typeID) : base(houseSize, tankSize, type, typeID) { }
        public override void SpecialService()
        {
            this.SrvMsg += "| Check Sunken Pump |";
            Console.WriteLine("...sunken pump checked...");

        }
    }
}
