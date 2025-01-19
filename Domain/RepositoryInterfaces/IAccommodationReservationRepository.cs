using BookingApp.Domain.Models;
using System;
using System.Collections.Generic;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IAccommodationReservationRepository
    {
        List<AccommodationReservation> GetAll();
        AccommodationReservation GetById(int id);
        AccommodationReservation Save(AccommodationReservation accommodationReservation);
        void Delete(AccommodationReservation accommodationReservation);
        AccommodationReservation Update(AccommodationReservation accommodationReservation);
        List<AccommodationReservation> GetByAccommodation(Accommodation accommodation);
        List<AccommodationReservation> GetByUser(User user);
        AccommodationReservation GetAccommodationReservationById(int id);
        List<AccommodationReservation> GetReservationsWithinNDays(int period, List<AccommodationReservation> accommodationReservations);
        AccommodationReservation Get(int id);
    }
}
