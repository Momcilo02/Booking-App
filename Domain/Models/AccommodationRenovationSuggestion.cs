using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Models
{
    public class AccommodationRenovationSuggestion : ISerializable
    {
        public int Id { get; set; }
        public Accommodation Accommodation {  get; set; }
        public int Urgency{ get; set; }
        public string Comment { get; set; }
        public AccommodationRenovationSuggestion()
        {
            
        }
        public AccommodationRenovationSuggestion(Accommodation accommodation, int urgency, string comment)
        {
            Accommodation = accommodation;
            Urgency = urgency;
            Comment = comment;
        }
        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Accommodation = new Accommodation() { Id = Convert.ToInt32(values[1]) };
            Urgency = Convert.ToInt32(values[2]);
            Comment = Convert.ToString(values[3]);
        }

        public string[] ToCSV()
        {
            string[] values = {Id.ToString(), Accommodation.Id.ToString(),  Urgency.ToString(), Comment};
            return values;
        }
    }
}
