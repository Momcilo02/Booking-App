using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BookingApp.Domain.Models
{
    public class TouristReview : ISerializable
    {

        public int Id { get; set; }
        public int UserId { get; set; }
        public int TourReservationId { get; set; }
        public int GuideKnowledge { get; set; }
        public int GuideLanguage { get; set; }
        public int TourInterest { get; set; }
        public string Comment { get; set; }
        public List<Image>? Images { get; set; }
        public TouristReview() { }

        public int IsValid { get; set; }
        public TouristReview(int id, int userId, int tourReservationId, int guideKnowledge, int guideLanguage, int tourInterest, string comment)
        {
            Id = id;
            UserId = userId;
            TourReservationId = tourReservationId;
            GuideKnowledge = guideKnowledge;
            GuideLanguage = guideLanguage;
            TourInterest = tourInterest;
            Comment = comment;
            Images = new List<Image>();
            IsValid = 1;
        }
        public TouristReview(int userId, int tourReservationId, int guideKnowledge, int guideLanguage, int tourInterest, string comment)
        {
            UserId = userId;
            TourReservationId = tourReservationId;
            GuideKnowledge = guideKnowledge;
            GuideLanguage = guideLanguage;
            TourInterest = tourInterest;
            Comment = comment;
            Images = new List<Image>();
            IsValid = 1;
        }
        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            UserId = Convert.ToInt32(values[1]);
            TourReservationId = Convert.ToInt32(values[2]);
            GuideKnowledge = Convert.ToInt32(values[3]);
            GuideLanguage = Convert.ToInt32(values[4]);
            TourInterest = Convert.ToInt32(values[5]);
            Comment = values[6];
            IsValid = Convert.ToInt32(values[7]);
        }
        public string[] ToCSV()
        {
            string[] values = { Id.ToString(), UserId.ToString(), TourReservationId.ToString(), GuideKnowledge.ToString(), GuideLanguage.ToString(), TourInterest.ToString(), Comment,IsValid.ToString() };
            return values;
        }
    }
}
