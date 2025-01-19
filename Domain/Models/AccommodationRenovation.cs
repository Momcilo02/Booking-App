using BookingApp.DTO;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Models
{
    public class AccommodationRenovation: ISerializable
    {
        public int Id { get; set; }
        public int AccommodationId { get; set; }
        public DateTime FirstDay { get; set; }
        public DateTime LastDay { get; set; }
        public string Comment { get; set; }



        public AccommodationRenovation()
        {
          
        }

        public AccommodationRenovation(int accommodationId, DateTime firstDay, DateTime lastDay, string comment)
        {
            AccommodationId = accommodationId;
            FirstDay = firstDay;
            LastDay = lastDay;
            Comment = comment;
        }
        public AccommodationRenovation(int id,int accommodationId, DateTime firstDay, DateTime lastDay, string comment)
        {
            Id = id;
            AccommodationId = accommodationId;
            FirstDay = firstDay;
            LastDay = lastDay;
            Comment = comment;
        }


        public AccommodationRenovationDTO ToDTO(AccommodationRenovation acc) 
        {
            return new AccommodationRenovationDTO(acc);
        }
        public string[] ToCSV()
        {
            string[] csvValues = {
            Id.ToString(),
            AccommodationId.ToString(),
            FirstDay.ToString(),
            LastDay.ToString(),
            Comment
        };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            int i = 0;
            Id = Convert.ToInt32(values[i++]);
            AccommodationId = Convert.ToInt32(values[i++]);
            FirstDay = DateTime.Parse(values[i++]);
            LastDay = DateTime.Parse(values[i++]);
            Comment = values[i++];
        }
    }
}
