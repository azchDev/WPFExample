using System;

namespace WPFExample
{
    public class Schedule : InterfaceSchedule
    {
        private string slot;
        private int slotID;
        private Customer customer;

        public string Slot { get => slot; set => slot = value; }

        public int SlotID { get => slotID; set => slotID = value; }
        public Customer Customer { get => customer; set => customer = value; }

        public Schedule() { }
        public Schedule(string slot, int slotID, Customer customer)
        {
            this.slot = slot;
            this.slotID = slotID;
            this.customer = customer;
        }
        public override string ToString()
        {
            return string.Format($"Your appointment is scheduled at {Slot}");
        }


        public int CompareTo(InterfaceSchedule aptm)
        {
            return slotID.CompareTo(aptm.SlotID);
        }

        public bool Equals(InterfaceSchedule aptm)
        {
            if (aptm == null)
            {
                return false;
            }
            return slotID.Equals(aptm.SlotID);
        }


    }
}