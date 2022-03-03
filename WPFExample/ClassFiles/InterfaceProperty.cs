

namespace WPFExample
{
    public delegate void PerformingServices();
    interface InterfaceProperty
    {
        int HouseSize { get; set; }
        int TankSize { get; set; }
        string Type { get; set; }
        int TypeID { get; set; }
        string SrvMsg { get; set; }
        PerformingServices PS { get; set; }
        void CheckPipes();
        void Tighten();
        void Insulate();
        void SpecialService();

    }
}
