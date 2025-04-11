using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_Bedat.Models
{
    public enum EventType
    {
        Belépés = 1,
        Kilépés,
        Menza,
        Könyvtár
        
    }
    public class Esemeny
    {
        public Esemeny(string lines)
        {
            string[] line = lines.Split(" ");
            Id = line[0];
            EventTime = TimeSpan.Parse(line[1]);
            EventType = (EventType)int.Parse(line[2]);
            IsItAfterNoon = EventTime > TimeSpan.FromHours(12);
        }

        public string Id { get; set; }
        public TimeSpan EventTime { get; set; }
        public EventType EventType { get; set; }
        public bool IsItAfterNoon { get; set; }
    }
}
