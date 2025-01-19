using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Models
{
    public class CheckPoint : ISerializable
    {
        public int Id { get; set; }
        public int Order { get; set; }
        public string Name { get; set; }
        public int TourId { get; set; }

        public CheckPoint()
        {

        }

        public CheckPoint(int order, string name, int tourId)
        {
            Order = order;
            Name = name;
            TourId = tourId;
        }

        public CheckPoint(int id, int order, string name, int tourId)
        {
            Id = id;
            Order = order;
            Name = name;
            TourId = tourId;
        }

        public string[] ToCSV()
        {
            string[] values = { Id.ToString(), Order.ToString(), Name, TourId.ToString() };
            return values;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Order = Convert.ToInt32(values[1]);
            Name = Convert.ToString(values[2]);
            TourId = Convert.ToInt32(values[3]);

        }

    }
}
