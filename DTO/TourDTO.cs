using BookingApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTO
{
    public class TourDTO : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public int Id { get; set; }
        public int TourReservationId { get; set; }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                if (value != name)
                {
                    name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }
        private DateTime date;
        public DateTime Date
        {
            get { return date; }
            set
            {
                if (value != date)
                {
                    date = value;
                    OnPropertyChanged(nameof(Date));
                }
            }
        }
        private ObservableCollection<TourGuestDTO> _tourGuests;
        public ObservableCollection<TourGuestDTO> TourGuests
        {
            get => _tourGuests;
            set
            {
                if (_tourGuests != value)
                {
                    _tourGuests = value;
                    OnPropertyChanged(nameof(TourGuests));
                }
            }
        }
        private Location location;
        public Location Location
        {
            get { return location; }
            set
            {
                if (value != location)
                {
                    location = value;
                    OnPropertyChanged(nameof(Location));
                }
            }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set
            {
                if (value != description)
                {
                    description = value;
                    OnPropertyChanged(nameof(Description));
                }
            }
        }

        private Language language;
        public Language Language
        {
            get { return language; }
            set
            {
                if (value != language)
                {
                    language = value;
                    OnPropertyChanged(nameof(Language));
                }
            }
        }

        private int maxGuests;
        public int MaxGuests
        {
            get { return maxGuests; }
            set
            {
                if (value != maxGuests)
                {
                    maxGuests = value;
                    OnPropertyChanged(nameof(MaxGuests));
                }
            }
        }

        private CheckPoint checkPoint;
        public CheckPoint CheckPoint
        {
            get { return checkPoint; }
            set
            {
                if (value != checkPoint)
                {
                    checkPoint = value;
                    OnPropertyChanged(nameof(CheckPoint));
                }
            }
        }

        private TourTime tourTime;
        public TourTime TourTime
        {
            get { return tourTime; }
            set
            {
                if (value != tourTime)
                {
                    tourTime = value;
                    OnPropertyChanged(nameof(TourTime));
                }
            }
        }

        private double duration;
        public double Duration
        {
            get { return duration; }
            set
            {
                if (value != duration)
                {
                    duration = value;
                    OnPropertyChanged(nameof(Duration));
                }
            }
        }
        private string imagePath;
        public string ImagePath
        {
            get { return imagePath; }
            set
            {
                if (value != imagePath)
                {
                    imagePath = value;
                    OnPropertyChanged(nameof(ImagePath));
                }
            }
        }

        public TourDTO()
        {
            TourGuests = new ObservableCollection<TourGuestDTO>();
        }
        public TourDTO(Tour tour) : this()
        {
            Id = tour.Id;
            Name = tour.Name;
            Location = tour.Location;
            Description = tour.Description;
            Language = tour.Language;
            MaxGuests = tour.MaxGuests;
            CheckPoint = tour.CheckPoint;
            TourTime = tour.TourTime;
            Duration = tour.Duration;
            if (tour.ImagePaths != null && tour.ImagePaths.Count > 0)
            {
                ImagePath = tour.ImagePaths[0];
            }
            else
            {
                ImagePath = "/Resources/Images/user.png";
            }
        }
        public Tour ToTour()
        {
            return new Tour(Id, name, location, description, language, maxGuests, checkPoint, tourTime, duration);
        }

        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
