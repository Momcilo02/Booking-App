using BookingApp.Domain.Models;
using System.Collections.Generic;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IAccommodationReservationRequestRepository
    {
        List<AccommodationReservationRequest> GetAll();
        AccommodationReservationRequest GetById(int id);
        AccommodationReservationRequest Save(AccommodationReservationRequest accommodationReservationRequest);
        AccommodationReservationRequest Delete(AccommodationReservationRequest request);
        AccommodationReservationRequest Update(AccommodationReservationRequest accommodationReservationRequest);
        
    }
}
