using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BookingApp.Domain.Models
{
    public class LiveTour : ISerializable
    {
        public int Id { get; set; }
        public int TourId { get; set; }
        public int TourTimeId { get; set; }
        public int IsFinished { get; set; }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            TourId = Convert.ToInt32(values[1]);
            TourTimeId = Convert.ToInt32(values[2]);
            IsFinished = Convert.ToInt32(values[3]);
        }

        public string[] ToCSV()
        {
            string[] values = { Id.ToString(), TourId.ToString(), TourTimeId.ToString(), IsFinished.ToString() };
            return values;
        }
    }


}
