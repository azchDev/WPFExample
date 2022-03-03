using System;

namespace WPFExample
{
    public interface InterfaceSchedule : IComparable<InterfaceSchedule>, IEquatable<InterfaceSchedule>
    {
        string Slot { get; set; }
        int SlotID { get; set; }

        Customer Customer { get; set; }

    }
}
