using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BookingApp.Domain.Models
{
    public class GuestReview : ISerializable
    {
        public int Id { get; set; }
        public AccommodationReservation AccommodationReservation { get; set; }
        public int Cleaness { get; set; }
        public int Correctness { get; set; }
        public string Comment { get; set; }

        public GuestReview()
        {

        }
        public GuestReview(AccommodationReservation accommodationReservation, int cleanness, int correctness, string commnet)
        {
            AccommodationReservation = accommodationReservation;
            Cleaness = cleanness;
            Correctness = correctness;
            Comment = commnet;
        }
        public GuestReview(int id, AccommodationReservation accommodationReservation, int cleanness, int correctness, string commnet)
        {
            Id = id;
            AccommodationReservation = accommodationReservation;
            Cleaness = cleanness;
            Correctness = correctness;
            Comment = commnet;
        }

        public string[] ToCSV()
        {
            string[] values = { Id.ToString(), AccommodationReservation.Id.ToString(), Cleaness.ToString(), Correctness.ToString(), Comment };
            return values;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            AccommodationReservation = new AccommodationReservation() { Id = Convert.ToInt32(values[1]) };
            Cleaness = Convert.ToInt32(values[2]);
            Correctness = Convert.ToInt32(values[3]);
            Comment = values[4];
        }
    }
}
