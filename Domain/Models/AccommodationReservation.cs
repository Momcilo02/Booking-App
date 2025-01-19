using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Models
{
    public class AccommodationReservation : ISerializable
    {
        public int Id { get; set; }
        public Accommodation Accommodation { get; set; }
        public User User { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public int NumberOfPeople { get; set; }
        //public int Duration { get; set; }
        public AccommodationReservation()
        {

        }

        public AccommodationReservation(Accommodation accommodation, User user, DateOnly startDate, DateOnly endDate, int numberOfPeople)
        {
            Accommodation = accommodation;
            User = user;
            StartDate = startDate;
            EndDate = endDate;
            NumberOfPeople = numberOfPeople;
            //Duration = EndDate.DayNumber - StartDate.DayNumber;
        }
        public AccommodationReservation(int id, Accommodation accommodation, User user, DateOnly startDate, DateOnly endDate, int numberOfPeople)
        {
            Id = id;
            Accommodation = accommodation;
            User = user;
            StartDate = startDate;
            EndDate = endDate;
            NumberOfPeople = numberOfPeople;
            //Duration = EndDate.DayNumber - StartDate.DayNumber;
        }

        public string[] ToCSV()
        {
            string[] values = { Id.ToString(), Accommodation.Id.ToString(), User.Id.ToString(), StartDate.ToString(), EndDate.ToString(), NumberOfPeople.ToString() };
            return values;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Accommodation = new Accommodation() { Id = Convert.ToInt32(values[1]) };
            User = new User() { Id = Convert.ToInt32(values[2]) };
            StartDate = DateOnly.Parse(values[3]);
            //StartDate = DateOnly.ParseExact(values[3], "dd/MM/yyyy");
            //EndDate = DateOnly.ParseExact(values[4], "dd/MM/yyyy");
            EndDate = DateOnly.Parse(values[4]);
            NumberOfPeople = Convert.ToInt32(values[5]);
        }
    }
}
