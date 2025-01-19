using BookingApp.Application.Injector;
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
    public class AccommodationReservationRequestService
    {
        private readonly IAccommodationReservationRequestRepository accommodationReservationRequestRepository;
        
        public AccommodationReservationRequestService()
        {
            accommodationReservationRequestRepository = Injector.Injector.CreateInstance<IAccommodationReservationRequestRepository>();
        }
        public List<AccommodationReservationRequest> GetAll()
        {
            return accommodationReservationRequestRepository.GetAll();
        }
        public AccommodationReservationRequest Save(AccommodationReservationRequest request)
        {
            return accommodationReservationRequestRepository.Save(request);
        }
        public AccommodationReservationRequest Delete(AccommodationReservationRequest request)
        {
            return accommodationReservationRequestRepository.Delete(request);
        }
        public AccommodationReservationRequest Update(AccommodationReservationRequest request)
        {
            return accommodationReservationRequestRepository.Update(request);
        }
        public List<int> GetAllRejectedOrApprovedIds()
        {
            List<int> ids = new List<int>();
            foreach (var request in accommodationReservationRequestRepository.GetAll())
            {
                if (request.Status == Enumeration.AccommodationReservationRquest.Rejected || request.Status == Enumeration.AccommodationReservationRquest.Approved)
                    ids.Add(request.OldReservation.Id);
            }
            return ids;
        }
    }
}
