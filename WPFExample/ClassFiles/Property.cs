using System;
using System.Xml.Serialization;

namespace WPFExample
{

    public abstract class Property : InterfaceProperty
    {
        private int houseSize;
        private int tankSize;
        private string type;
        private int typeID;
        private PerformingServices ps;
        private string srvMsg;

        public int HouseSize { get => houseSize; set => houseSize = value; }
        public int TankSize { get => tankSize; set => tankSize = value; }
        public string Type { get => type; set => type = value; }
        public int TypeID { get => typeID; set => typeID = value; }

        public string SrvMsg { get => srvMsg; set => srvMsg = value; }
        [XmlIgnore]
        public PerformingServices PS { get => ps; set => ps = value; }

        public Property() {
            ps = CheckPipes;
            ps += Tighten;
            ps += Insulate;
            ps += SpecialService;
            srvMsg = "";
        }
        public Property(int houseSize, int tankSize, string type, int typeID)
        {
            this.houseSize = houseSize;
            this.tankSize = tankSize;
            this.type = type;
            this.typeID = typeID;
            ps = CheckPipes;
            ps += Tighten;
            ps += Insulate;
            ps += SpecialService;
            this.srvMsg = "";
        }

        public void CheckPipes()
        {
            SrvMsg += "| Check Pipes |";
            Console.WriteLine("...pipes checked...");
        }
        public void Tighten()
        {
            SrvMsg += "| Tighten Pipes |";
            Console.WriteLine("...pipes tightened...");
        }
        public void Insulate()
        {
            SrvMsg += "| Insulate Pipes |";
            Console.WriteLine("...pipes insulated...");
        }
        public abstract void SpecialService();

        public override string ToString()
        {
            return string.Format($"House Size: {HouseSize}sq. ft. - Tank Size: {TankSize}gal. - Type: {Type} |");
        }

    }
}
