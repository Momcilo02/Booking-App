using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace BookingApp.Domain.Models
{
    public class TourRequest : ISerializable
    {
        public int Id { get; set; }
        public int TouristId { get; set; }
        public int GuideId { get; set; }
        public Location Location { get; set; }
        public string Description { get; set; }
        public Language Language { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public int Status { get; set; }
        public int GuestNumber { get; set; }
        public DateOnly FinalDate { get; set; }
        public int ComplexTourId { get; set; }
        public TourRequest() { }
        public TourRequest(int id, int touristId, Location location, string description, Language language, DateOnly startDate, DateOnly endDate, int status, int guestNumber, DateOnly finalDate, int guideId)
        {
            Id = id;
            TouristId = touristId;
            Location = location;
            Description = description;
            Language = language;
            StartDate = startDate;
            EndDate = endDate;
            Status = status;
            GuestNumber = guestNumber;
            FinalDate = DateOnly.FromDateTime(DateTime.Today);
            GuideId = guideId;
            ComplexTourId = 0;

        }
        public TourRequest(int id, int touristId, Location location, string description, Language language, DateOnly startDate, DateOnly endDate, int status, int guestNumber, DateOnly finalDate, int guideId, int complexTourId)
        {
            Id = id;
            TouristId = touristId;
            Location = location;
            Description = description;
            Language = language;
            StartDate = startDate;
            EndDate = endDate;
            Status = status;
            GuestNumber = guestNumber;
            FinalDate = DateOnly.FromDateTime(DateTime.Today);
            GuideId = guideId;
            ComplexTourId = complexTourId;

        }
        public string[] ToCSV()
        {
            string[] values = { Id.ToString(), TouristId.ToString(), Location.Id.ToString(), Description.ToString(), Language.Id.ToString(), StartDate.ToString(), EndDate.ToString(), Status.ToString(), GuestNumber.ToString(), FinalDate.ToString(), GuideId.ToString(), ComplexTourId.ToString() };
            return values;
        }
        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            TouristId = Convert.ToInt32(values[1]);
            Location = new Location() { Id = Convert.ToInt32(values[2]) };
            Description = Convert.ToString(values[3]);
            Language = new Language() { Id = Convert.ToInt32(values[4]) };
            StartDate = DateOnly.Parse(values[5]);
            EndDate = DateOnly.Parse(values[6]);
            Status = Convert.ToInt32(values[7]);
            GuestNumber = Convert.ToInt32(values[8]);
            FinalDate = DateOnly.Parse(values[9]);
            GuideId = Convert.ToInt32(values[10]);
            ComplexTourId = Convert.ToInt32(values[11]);
        }
    }
}
