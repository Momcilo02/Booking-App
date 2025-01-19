using BookingApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IAccommodationStatsRepository
    {
        List<AccommodationStats> GetAll();
        AccommodationStats GetById(int id);
        AccommodationStats Save(AccommodationStats accommodation);
        void Delete(AccommodationStats accommodation);
        AccommodationStats Update(AccommodationStats accommodation);

    }
}
