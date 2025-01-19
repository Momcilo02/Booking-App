using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.UseCases
{
    public class AccommodationRenovationService
    {
        private readonly IAccommodationRenovationRepository accommodationRenovationRepository;
        private AccommodationReservationService accommodationReservationService;
        public AccommodationRenovationService()
        {
            accommodationReservationService = new AccommodationReservationService();
            accommodationRenovationRepository = Injector.Injector.CreateInstance<IAccommodationRenovationRepository>();
        }
        public void Delete(AccommodationRenovation accommodation) 
        {
            accommodationRenovationRepository.Delete(accommodation);
        }
        public List<AccommodationRenovation> GetAll()
        {
            return accommodationRenovationRepository.GetAll();
        }
        public AccommodationRenovation Save(AccommodationRenovation accommodation)
        { 
            return accommodationRenovationRepository.Save(accommodation);
        }
        public List<DateTime> FindAvailableRenovationDates(DateTime startDate, DateTime endDate, int renovationDuration, int accommodationId)
        {
            // Dohvatanje svih rezervacija za odabrani smještaj
            var reservations = accommodationReservationService.GetAllReservationsForAccommodation(accommodationId);

            // Inicijalizacija liste dostupnih datuma za renoviranje
            var availableDates = new List<DateTime>();

            // Iteriranje kroz sve datume unutar odabranog opsega
            for (var date = startDate; date <= endDate; date = date.AddDays(1))
            {
                // Provera da li je trenutni datum rezervisan
                DateOnly StartDateOnly = new DateOnly(startDate.Year, startDate.Month, startDate.Day);
                DateOnly EndDateOnly = new DateOnly(endDate.Year, endDate.Month, endDate.Day);
                if (!reservations.Any(r => StartDateOnly >= r.StartDate && EndDateOnly <= r.EndDate))
                {
                    // Provera da li postoji dovoljno slobodnih dana za renoviranje
                    if (IsAvailableRenovationDuration(date, endDate, renovationDuration, reservations))
                    {
                        // Dodavanje trenutnog datuma u listu dostupnih datuma za renoviranje
                        availableDates.Add(date);
                    }
                }
            }

            return availableDates;
        }

        private bool IsAvailableRenovationDuration(DateTime startDate, DateTime endDate, int renovationDuration, List<AccommodationReservation> reservations)
        {
            // Iteriranje kroz sve datume unutar trajanja renoviranja
            for (var date = startDate; date < startDate.AddDays(renovationDuration); date = date.AddDays(1))
            {
                // Provera da li je trenutni datum rezervisan
                DateOnly StartDateOnly = new DateOnly(startDate.Year, startDate.Month, startDate.Day);
                DateOnly EndDateOnly = new DateOnly(endDate.Year, endDate.Month, endDate.Day);
                if (reservations.Any(r => StartDateOnly >= r.StartDate && EndDateOnly <= r.EndDate))
                {
                    return false;
                }
            }
            return true;
        }
    }
    
}
