using BookingApp.Domain.Models;
using System.Collections.Generic;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface ITourRepository
    {
        List<Tour> GetAll();
        Tour Get(int id);
        int NextId();
        Tour Save(Tour tour);
        void Delete(Tour tour);
        Tour Update(Tour tour);
        Tour UpdateCapacity(int tourId, int maxGuests);

    }
}
