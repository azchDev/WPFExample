using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace WPFExample
{
    [XmlRoot("ScheduleList")]
    [XmlInclude(typeof(City))]
    [XmlInclude(typeof(Rural))]
    [XmlInclude(typeof(LRural))]
    [XmlInclude(typeof(Customer))]
    public class EventsIdx : IDisposable, IEnumerable<Schedule>
    {
        private List<Schedule> scheduleList = null;

        [XmlArray("Appointments")]
        [XmlArrayItem("Schedule")]

        public List<Schedule> ScheduleList { get => scheduleList; set => scheduleList = value; }
  
        public EventsIdx()
        {
            scheduleList = new List<Schedule>();
        }
        public Schedule this[int i]
        {
            get { return scheduleList[i]; }
            set { scheduleList[i] = value; }
        }
        public int Count
        {
            get { return scheduleList.Count; }
        }
        public void Add(Schedule schedule)
        {
            scheduleList.Add(schedule);
        }
        public void Sort() {
            scheduleList.Sort();
        }
        public bool Contains(Schedule schedule)
        {
            return scheduleList.Contains(schedule);
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public IEnumerator<Schedule> GetEnumerator()
        {
            return ((IEnumerable<Schedule>)scheduleList).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<Schedule>)scheduleList).GetEnumerator();
        }
    }
}
