using BookingApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IAccommodationRenovationRepository
    {
        List<AccommodationRenovation> GetAll();
        AccommodationRenovation GetById(int id);
        AccommodationRenovation Save(AccommodationRenovation accommodation);
        void Delete(AccommodationRenovation accommodation);
        AccommodationRenovation Update(AccommodationRenovation accommodation);
        
    }
}
