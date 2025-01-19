using BookingApp.Domain.Models;
using System.Collections.Generic;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IAccommodationRepository
    {
        List<Accommodation> GetAll();
        Accommodation GetById(int id);
        Accommodation Save(Accommodation accommodation);
        void Delete(Accommodation accommodation);
        Accommodation Update(Accommodation accommodation);
        List<Accommodation> GetAccommodationsByOwnerId(int id);
        Accommodation GetAccommodation(int id);
    }
}
