using BookingApp.Application.Injector;
using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.DTO;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.UseCases
{
    public class LocationService
    {
        private readonly ILocationRepository _locationRepository;
        public LocationService()
        {
            _locationRepository = Injector.Injector.CreateInstance<ILocationRepository>();
        }

        public List<Location> GetAll()
        {
            return _locationRepository.GetAll();
        }

        public Location Save(Location tourGuest)
        {
            return _locationRepository.Save(tourGuest);
        }

        public List<string> GetCities()
        {
            return _locationRepository.GetCities();
        }
        public List<string> GetCountries()
        {
            return _locationRepository.GetCountries();
        }
        public List<string> GetCitiesAndCountries()
        {           
            return _locationRepository.GetCitiesAndCountries();
        }
        public Location Get(int id)
        {
            return _locationRepository.Get(id);
        }
        public ObservableCollection<LocationDTO> InitializeLocations()
        {
            var locations = new ObservableCollection<LocationDTO>();

            foreach (Location location in GetAll())
            {
                locations.Add(new LocationDTO(location));
            }

            return locations;
        }
        public string GetLocationName(int id)
        {
            Location language = _locationRepository.Get(id);
            string name = language.City+" "+language.Country;
            return name;
        }
        /*public int GetLocationIdByCityAndCountry(List<string> cityAndCountry)
        {
            int id = 0;
            for (int i = 0; i <= cityAndCountry.Count(); i++)
            {
                var split = cityAndCountry[i].Split(',');
                foreach (Location location in GetAll())
                {
                    if (split[0]==)
                }
            }  
        }*/
    }
}
