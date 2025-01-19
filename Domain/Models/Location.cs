using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Models
{
    public class Location : ISerializable
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }

        public Location()
        {

        }
        public Location(string country, string city)
        {

            Country = country;
            City = city;
        }
        public Location(int id, string country, string city)
        {
            Id = id;
            Country = country;
            City = city;
        }

        public string[] ToCSV()
        {
            string[] values = { Id.ToString(), Country, City };
            return values;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Country = Convert.ToString(values[1]);
            City = Convert.ToString(values[2]);
        }
    }
}
