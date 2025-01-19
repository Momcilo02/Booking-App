using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BookingApp.Domain.Models
{
    public class Accommodation : ISerializable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Location Location { get; set; }
        public Enumeration.AccommodationType Type { get; set; }
        public int MaxGuests { get; set; }
        public int MinReservationDays { get; set; }
        public int UncancellablePeriod { get; set; }//Number of days before reservation where you can't cancel
        public List<Image>? Images { get; set; }
        public User Owner { get; set; }
        public Accommodation()
        {

        }

        //public Accommodation(string name, Location location, Enumeration.AccommodationType type, int maxGuests, int minReservation)
        //{ 
        //    this.Name = name;
        //    this.Location = location;
        //    this.Type = type;
        //    this.MaxGuests = maxGuests;
        //    this.MinReservationDays = minReservation;
        //    this.UncancellablePeriod = 1;
        //    this.Images = new List<Image>();
        //}
        public Accommodation(string name, Location location, Enumeration.AccommodationType type, int maxGuests, int minReservation, int uncancellablePreriod, User owner)
        {
            Name = name;
            Location = location;
            Type = type;
            MaxGuests = maxGuests;
            MinReservationDays = minReservation;
            UncancellablePeriod = uncancellablePreriod;
            Images = new List<Image>();
            Owner = owner;
        }
        public Accommodation(int id, string name, Location location, Enumeration.AccommodationType type, int maxGuests, int minReservation, int uncancellablePreriod, User owner)
        {
            Id = id;
            Name = name;
            Location = location;
            Type = type;
            MaxGuests = maxGuests;
            MinReservationDays = minReservation;
            UncancellablePeriod = uncancellablePreriod;
            Images = new List<Image>();
            Owner = owner;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            Location = new Location() { Id = Convert.ToInt32(values[2]) };
            Enum.TryParse(values[3], out Enumeration.AccommodationType Type);
            MaxGuests = Convert.ToInt32(values[4]);
            MinReservationDays = Convert.ToInt32(values[5]);
            UncancellablePeriod = Convert.ToInt32(values[6]);
            Owner = new User() { Id = Convert.ToInt32(values[7]) };
        }
        public string[] ToCSV()
        {
            string[] values = { Id.ToString(), Name.ToString(), Location.Id.ToString(), Type.ToString(), MaxGuests.ToString(), MinReservationDays.ToString(), UncancellablePeriod.ToString(), Owner.Id.ToString() };
            return values;
        }
    }
}
