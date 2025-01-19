using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Models
{
    public class AccommodationReview : ISerializable
    {
        public int Id { get; set; }
        public User Guest { get; set; }
        public Accommodation Accommodation { get; set; }
        public int Cleanness { get; set; }
        public int Correctness { get; set; }
        public string Comment { get; set; }
        public List<Image> Images { get; set; }

        public AccommodationReview()
        {

        }
        public AccommodationReview(int id, User guest, Accommodation accommodation, int cleanness, int correctness, string comment, List<Image> images)
        {
            Id = id;
            Guest = guest;
            Accommodation = accommodation;
            Cleanness = cleanness;
            Correctness = correctness;
            Comment = comment;
            Images = images;

        }
        public AccommodationReview(User guest, Accommodation accommodation, int cleanness, int correctness, string comment, List<Image> images)
        {

            Guest = guest;
            Accommodation = accommodation;
            Cleanness = cleanness;
            Correctness = correctness;
            Comment = comment;
            Images = images;
        }
        public AccommodationReview(User guest, Accommodation accommodation, int cleanness, int correctness, string comment)
        {

            Guest = guest;
            Accommodation = accommodation;
            Cleanness = cleanness;
            Correctness = correctness;
            Comment = comment;
            Images = new List<Image>();
        }

        public string[] ToCSV()
        {
            string[] values = { Id.ToString(), Guest.Id.ToString(), Accommodation.Id.ToString(), Cleanness.ToString(), Correctness.ToString(), Comment };
            return values;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Guest = new User() { Id = Convert.ToInt32(values[1]) };
            Accommodation = new Accommodation() { Id = Convert.ToInt32(values[2]) };
            Cleanness = Convert.ToInt32(values[3]);
            Correctness = Convert.ToInt32(values[4]);
            Comment = values[5];
        }
    }
}
