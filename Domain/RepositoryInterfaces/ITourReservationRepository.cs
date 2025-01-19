using BookingApp.Domain.Models;
using System.Collections.Generic;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface ITourReservationRepository
    {
        List<TourReservation> GetAll();
        TourReservation Save(TourReservation tourReservation);
        int NextId();
        void Delete(TourReservation tourReservation);
        TourReservation Update(TourReservation tourReservation);
        TourReservation Get(int id);
    }
}
