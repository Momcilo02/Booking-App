using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BookingApp.Domain.Models
{
    public class AccommodationStats : ISerializable
    {
        public int Id { get; set; }
        public int Reservations { get; set; }
        public int Cancellations { get; set; }
        public int Reschedulings { get; set; }
        public int RenovationRecommendations { get; set; }
       public AccommodationStats() { }
        public AccommodationStats(int id, int reservations,int cancellations, int reschedulings, int renovationRecommendations)
        {
            Id = id;
            Reservations = reservations;
            Cancellations = cancellations;
            Reschedulings = reschedulings;
            RenovationRecommendations = renovationRecommendations;
        }
        public AccommodationStats(int reservations, int cancellations, int reschedulings, int renovationRecommendations)
        {
            Reservations = reservations;
            Cancellations = cancellations;
            Reschedulings = reschedulings;
            RenovationRecommendations = renovationRecommendations;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Reservations = Convert.ToInt32(values[1]);
            Cancellations = Convert.ToInt32(values[2]);
            Reschedulings = Convert.ToInt32(values[3]);
            RenovationRecommendations = Convert.ToInt32(values[4]);
        }
        public string[] ToCSV()
        {
            string[] values = { Id.ToString(), Reservations.ToString(), Cancellations.ToString(), Reschedulings.ToString(), RenovationRecommendations.ToString()};
            return values;
        }
    }
}
