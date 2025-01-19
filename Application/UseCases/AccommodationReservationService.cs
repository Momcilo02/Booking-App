using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.DTO;
using BookingApp.Repository;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.UseCases
{
    public class AccommodationReservationService
    {
        
        private readonly IAccommodationReservationRepository accommodationReservationRepository;

        public AccommodationReservationService()
        {
            accommodationReservationRepository = Injector.Injector.CreateInstance<IAccommodationReservationRepository>();
        }

        public void CancelReservation(AccommodationReservationDTO reservation)
        {
            accommodationReservationRepository.Delete(reservation.ToAccommodationReservation());
        }
        public List<AccommodationReservation> GetAll()
        {
            return accommodationReservationRepository.GetAll();
        }
        public AccommodationReservation Update(AccommodationReservation reservation)
        {
            return accommodationReservationRepository.Update(reservation);
        }
        public AccommodationReservation Get(int id)
        {
            return accommodationReservationRepository.Get(id);
        }
        public AccommodationReservation Save(AccommodationReservation accommodetionReservation)
        {
            return accommodationReservationRepository.Save(accommodetionReservation);
        }

        public List<AccommodationReservationDTO> GetActiveReservations(int id)
        {
            List<AccommodationReservationDTO> reservations = new List<AccommodationReservationDTO>();
            foreach (var res in accommodationReservationRepository.GetAll())
            {
                if (res.StartDate.ToDateTime(TimeOnly.Parse("10:00PM")).AddDays(-res.Accommodation.UncancellablePeriod) > DateTime.Today && res.User.Id == id)
                {
                    reservations.Add(new AccommodationReservationDTO(res));
                }
            }
            return reservations;
        }
        public List<AccommodationReservationDTO> GetPastReservations(int id)
        {
            List<AccommodationReservationDTO> reservations = new List<AccommodationReservationDTO>();

            foreach (var res in accommodationReservationRepository.GetAll())
            {
                if (res.EndDate.ToDateTime(TimeOnly.Parse("10:00PM")).AddDays(5) < DateTime.Today && res.User.Id == id)
                {
                    reservations.Add(new AccommodationReservationDTO(res));
                }
            }
            return reservations;
        }
        public List<AccommodationReservation> GetAllReservationsForAccommodation(int accommodationId)
        {
            // Filtriranje rezervacija samo za odabrani smještaj
            return accommodationReservationRepository.GetAll().Where(r => r.Accommodation.Id == accommodationId).ToList();
        }
        public List<AccommodationReservation> GetByUser(User user)
        {
            return accommodationReservationRepository.GetByUser(user);
        }

    }
}
