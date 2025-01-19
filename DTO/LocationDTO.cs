using BookingApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTO
{
    public class LocationDTO:INotifyPropertyChanged
    {
        public int Id { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        private string _city;
        public string City
        {
            get { return _city; }
            set
            {
                if (_city != value)
                {
                    _city = value;
                    OnPropertyChanged(nameof(City));
                }
            }
        }

        private string _country;
        public string Country
        {
            get { return _country; }
            set
            {
                if (_country != value)
                {
                    _country = value;
                    OnPropertyChanged(nameof(Country));
                }
            }
        }


        private string fullLocation;
        public string FullLocation
        {
            get { return fullLocation; }
            set
            {
                if(fullLocation != value)
                {
                    fullLocation = value;
                    OnPropertyChanged(nameof(FullLocation));
                }
            }
        }
        public LocationDTO()
        {
            
        }
        public LocationDTO(Location location)
        {
            Id = location.Id;
            City = location.City;
            Country = location.Country;
            FullLocation = location.City + ", " + location.Country;
        }

        public Location ToLocation()
        {
            string[] splits = FullLocation.Split(',');
            return new Location(splits[1], splits[0]);
        }
        public Location ToLocationV2()
        {
            return new Location(Country, City);
        }
        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        }
    }
}
