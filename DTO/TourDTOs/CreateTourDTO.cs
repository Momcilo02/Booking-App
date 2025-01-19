using BookingApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BookingApp.DTO.TourDTOs
{
    public class CreateTourDTO : INotifyPropertyChanged
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                if (value != _name)
                {
                    _name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        private List<string> _imagePaths;
        public List<string> ImagePaths
        {
            get { return _imagePaths; }
            set
            {
                if (value != _imagePaths)
                {
                    _imagePaths = value;
                    OnPropertyChanged("ImagePaths");
                }
            }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                if (value != _description)
                {
                    _description = value;
                    OnPropertyChanged("Description");
                }
            }
        }

        private LocationDTO _location;
        public LocationDTO Location
        {
            get { return _location; }
            set
            {
                if (value != _location)
                {
                    _location = value;
                    OnPropertyChanged("Location");
                }
            }
        }

        private LanguageDTO _language;
        public LanguageDTO Language
        {
            get { return _language; }
            set
            {
                if (value != _language)
                {
                    _language = value;
                    OnPropertyChanged("Language");
                }
            }
        }

        private int maxTourists;
        public int MaxTourists
        {
            get { return maxTourists; }
            set
            {
                if (value != maxTourists)
                {
                    maxTourists = value;
                    OnPropertyChanged("MaxTourists");
                }
            }
        }

        private List<TourTimeDTO> _tourTimeDTOs;
        public List<TourTimeDTO> TourTimeDTOs
        {
            get
            {
                return _tourTimeDTOs;
            }
            set
            {
                if (value != _tourTimeDTOs)
                {
                    _tourTimeDTOs = value;
                    OnPropertyChanged("TourTimeDTOs");
                }
            }
        }


        private double _duration;
        public double Duration
        {
            get { return _duration; }
            set
            {
                if (value != _duration)
                {
                    _duration = value;
                    OnPropertyChanged("Duration");
                }
            }
        }

        private List<CheckPointDTO> _checkPointDtos;
        public List<CheckPointDTO> CheckPointDtos
        {
            get { return _checkPointDtos; }
            set
            {
                if (value != _checkPointDtos)
                {
                    _checkPointDtos = value;
                    OnPropertyChanged("CheckPointDtos");
                }
            }
        }

        public CreateTourDTO()
        {
            CheckPointDtos = new List<CheckPointDTO>();
            ImagePaths = new List<string>();
            TourTimeDTOs = new List<TourTimeDTO>();
            Language = new LanguageDTO();
            Location = new LocationDTO();
        }


        public Tour ToTour() 
        {
            var tour = new Tour();
            tour.Name = Name;
            tour.Description = Description;
            tour.Location = new Location
            {
                Id = Location.Id,
                City = Location.City,
                Country = Location.Country
            };
            tour.Duration = Duration;
            tour.MaxGuests = MaxTourists;
            tour.Language = new Language { Id = Language.Id, Name = Language.Name };
            tour.ImagePaths = new List<string>(ImagePaths);
            return tour;
        }





        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
