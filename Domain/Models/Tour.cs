using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Models
{
    public class Tour : ISerializable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Location Location { get; set; }
        public string Description { get; set; }
        public Language Language { get; set; }
        public int MaxGuests { get; set; }
        public CheckPoint CheckPoint { get; set; }
        public TourTime TourTime { get; set; }
        public double Duration { get; set; }

        public int GuideId { get; set; }
        public List<Image>? Images { get; set; }

        public List<string> ImagePaths { get; set; }

        public Tour()
        {

        }

        public Tour(string name, Location location, string description, Language language, int maxGuests, double duration, List<string> imagePaths)
        {
            Name = name;
            Location = location;
            Description = description;
            Language = language;
            MaxGuests = maxGuests;
            Duration = duration;
            ImagePaths = new List<string>(imagePaths);
        }

        public Tour(int id, string name, Location location, string description, Language language, int maxGuests, CheckPoint checkPoint, TourTime tourTime, double duration)
        {
            Id = id;
            Name = name;
            Location = location;
            Description = description;
            Language = language;
            MaxGuests = maxGuests;
            CheckPoint = checkPoint;
            TourTime = tourTime;
            Duration = duration;
            Images = new List<Image>();
        }


        public string[] ToCSV()
        {
            string imagePaths = string.Join(",", ImagePaths);

            string[] values = { Id.ToString(), Name, Location.Id.ToString(), Description.ToString(), Language.Id.ToString(),
                                MaxGuests.ToString(), Duration.ToString(), GuideId.ToString(),imagePaths };
            return values;
        }


        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = Convert.ToString(values[1]);
            Location = new Location() { Id = Convert.ToInt32(values[2]) };
            Description = Convert.ToString(values[3]);
            Language = new Language() { Id = Convert.ToInt32(values[4]) };
            MaxGuests = Convert.ToInt32(values[5]);
            Duration = Convert.ToDouble(values[6]);
            GuideId = Convert.ToInt32(values[7]);
            ImagePaths = values[8].Split(',').ToList();

        }
    }
}
