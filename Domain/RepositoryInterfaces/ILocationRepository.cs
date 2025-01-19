using BookingApp.Domain.Models;
using System.Collections.Generic;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface ILocationRepository
    {
        List<Location> GetAll();
        Location Get(int id);
        List<string> GetCities();
        List<string> GetCountries();
        List<string> GetCitiesAndCountries();
        int NextId();
        Location Save(Location location);
    }
}
