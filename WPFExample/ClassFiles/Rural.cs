
using System;

namespace WPFExample
{

    public class Rural : Property
    {
        private bool well;

        public bool Well { get => well; set => well = value; }

        public Rural() { }
        public Rural(int houseSize, int tankSize, string type, int typeID, bool well) : base(houseSize, tankSize, type, typeID)
        {
            this.well = well;
        }

        public override void SpecialService()
        {
            if (Well)
            {
                this.SrvMsg += "| Check for sand in the water |";
                Console.WriteLine("...Sand in the water checked...");
            }
        }
    }
}
