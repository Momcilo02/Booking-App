using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class LocationRepository : ILocationRepository
    {
        private const string FilePath = "../../../Resources/Data/locations.csv";

        private readonly Serializer<Location> _serializer;

        private List<Location> _locations;
        public LocationRepository()
        {
            _serializer = new Serializer<Location>();
            _locations = _serializer.FromCSV(FilePath);
        }
        public List<Location> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }
        public Location Get(int id)
        {
            List<Location> locations = GetAll();
            return locations.Find(l => l.Id == id);
        }
        public List<string> GetCities()
        {
            var cities = _locations.Select(a => a.City).Distinct().ToList();
            return cities;
        }

        public List<string> GetCountries()
        {
            var countries = _locations.Select(a => a.Country).Distinct().ToList();
            return countries;
        }
        public List<string> GetCitiesAndCountries()
        {
            var citiesAndCountries = _locations.Select(a => $"{a.City}, {a.Country}").Distinct().ToList();
            return citiesAndCountries;
        }
        public int NextId()
        {
            _locations = _serializer.FromCSV(FilePath);
            if (_locations.Count < 1)
            {
                return 1;
            }
            return _locations.Max(x => x.Id) + 1;
        }
        public Location Save(Location location)
        {
            location.Id = NextId();
            _locations = _serializer.FromCSV(FilePath);
            _locations.Add(location);
            _serializer.ToCSV(FilePath, _locations);
            return location;
        }
    }
}
