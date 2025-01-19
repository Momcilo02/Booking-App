using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BookingApp.Domain.Models
{
    public class TourTime : ISerializable
    {
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public int MaxGuests { get; set; }
        public int TourId { get; set; }
        public bool Started { get; set; }
        public TourTime()
        {

        }

        public TourTime(int id, DateTime time, int maxGuests, int tourId, bool started)
        {
            Id = id;
            Time = time;
            MaxGuests = maxGuests;
            TourId = tourId;
            Started = started;
        }

        public TourTime(DateTime time, int maxGuests, int tourId, bool started)
        {
            Time = time;
            MaxGuests = maxGuests;
            TourId = tourId;
            Started = started;
        }

        public string[] ToCSV()
        {
            string[] values = { Id.ToString(), Time.ToString(), MaxGuests.ToString(), TourId.ToString(), Started.ToString() };
            return values;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Time = Convert.ToDateTime(values[1]);
            MaxGuests = Convert.ToInt32(values[2]);
            TourId = Convert.ToInt32(values[3]);
            Started = Convert.ToBoolean(values[4]);

        }
    }
}

