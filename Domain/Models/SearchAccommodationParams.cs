using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Models
{
    public class SearchAccommodationParams
    {
        public string Name { get; set; }
        public List<Enumeration.AccommodationType> Types { get; set; }
        public int LocationId { get; set; }
        public int MaxGuests { get; set; }
        public int StayLength { get; set; }
        public SearchAccommodationParams()
        {
            Types = new List<Enumeration.AccommodationType>();
        }
        public SearchAccommodationParams(string name, List<Enumeration.AccommodationType> types, int locationId, int maxGuests, int stayLength)
        {
            Name = name;
            Types = types;
            LocationId = locationId;
            MaxGuests = maxGuests;
            StayLength = stayLength;
        }
    }
}
