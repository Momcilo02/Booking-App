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
    public class AccommodationStatsService
    {
        private readonly IAccommodationStatsRepository accommodationStatsRepository;
        private AccommodationReservationService accommodationReservationService;
        private AccommodationReservationRequestRepository accommodationReservationRequestRepository;
        private AccommodationReservationRepository accommodationReservationRepository;


        public AccommodationStatsService()
        {
            accommodationStatsRepository = Injector.Injector.CreateInstance<IAccommodationStatsRepository>();
            accommodationReservationService = new AccommodationReservationService();
            accommodationReservationRepository= new AccommodationReservationRepository();
            accommodationReservationRequestRepository = new AccommodationReservationRequestRepository();
        }

        public void Delete(AccommodationStats accommodation)
        {
            accommodationStatsRepository.Delete(accommodation);
        }
        public List<AccommodationStats> GetAll()
        {
            return accommodationStatsRepository.GetAll();
        }
        public AccommodationStats Save(AccommodationStats accommodation)
        {
            return accommodationStatsRepository.Save(accommodation);
        }
        public int GetReservationCountForAccommodationYear(int accommodationId, int year)
        {
            return accommodationReservationRepository.GetReservationCountForAccommodationYear(accommodationId, year);
        }
        public int GetReservationCountForAccommodationMonth(int accommodationId, int year,int month)
        {
            return accommodationReservationRepository.GetReservationCountForAccommodationMonth(accommodationId, year,month);
        }
        public int GetCancellationCountForAccommodationYear(int accommodationId, int year) 
        {
            return accommodationReservationRequestRepository.GetCancellationCountForAccommodationYear(accommodationId, year);
        }
        public int GetCancellationCountForAccommodationMonth(int accommodationId, int year,int month) 
        {
            return accommodationReservationRequestRepository.GetCancellationCountForAccommodationMonth(accommodationId, year,month);

        }

    }
}
