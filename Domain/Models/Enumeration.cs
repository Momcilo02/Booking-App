using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Models
{
    public class Enumeration
    {
        public enum AccommodationReservationRquest
        {
            InProcess,
            Rejected,
            Approved
        }
        public enum AccommodationType
        {
            Apartment,
            House,
            Cottage
        }
        public enum EntityType
        {
            Accommodation,
            Tour,
            AccommodationReview
        }
        public enum UserType
        {
            Owner,
            Guest,
            Guide,
            Tourist,
            SuperOwner
        }
        public enum LanguageType
        {
            Serbian,
            English,
            French
        }

    }
}
