using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BookingApp.Domain.Models
{
    public class AccommodationReservationRequest : ISerializable
    {
        public int Id { get; set; }
        public AccommodationReservation OldReservation { get; set; }

        public DateOnly NewStartDate { get; set; }
        public DateOnly NewEndDate { get; set; }
        public Enumeration.AccommodationReservationRquest Status { get; set; }
        public string Comment { get; set; }

        public AccommodationReservationRequest()
        {

        }
        public AccommodationReservationRequest(AccommodationReservation accommodationReservation, DateOnly startDate, DateOnly endDate, Enumeration.AccommodationReservationRquest status, string comment)
        {
            OldReservation = accommodationReservation;
            NewStartDate = startDate;
            NewEndDate = endDate;
            Status = status;
            Comment = comment;

        }
        public AccommodationReservationRequest(int id, AccommodationReservation accommodationReservation, DateOnly startDate, DateOnly endDate, Enumeration.AccommodationReservationRquest status, string comment)
        {
            Id = id;
            OldReservation = accommodationReservation;
            NewStartDate = startDate;
            NewEndDate = endDate;
            Status = status;
            Comment = comment;

        }
        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            OldReservation = new AccommodationReservation() { Id = Convert.ToInt32(values[1]) };
            NewStartDate = DateOnly.Parse(values[2]);
            NewEndDate = DateOnly.Parse(values[3]);
            Status = (Enumeration.AccommodationReservationRquest)Convert.ToInt32(values[4]);
            Comment = values[5];
        }
        public string[] ToCSV()
        {

            string[] values = { Id.ToString(), OldReservation.Id.ToString(), NewStartDate.ToString(), NewEndDate.ToString(), ((int)Status).ToString(), Comment };

            return values;
        }

    }
}
