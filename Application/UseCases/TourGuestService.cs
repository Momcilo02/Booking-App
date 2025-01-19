using BookingApp.Application.Injector;
using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.UseCases
{
    public class TourGuestService
    {
        private readonly ITourGuestRepository _tourGuestRepository;
        
        public TourGuestService()
        {
            _tourGuestRepository = Injector.Injector.CreateInstance<ITourGuestRepository>();

        }
        public List<TourGuest> GetAll()
        {
            return _tourGuestRepository.GetAll();
        }
        public List<TourGuest> GetByTour(int id)
        {
            return _tourGuestRepository.GetAll().Where(x => x.TourReservationId== id).ToList();
        }
        public TourGuest Save(TourGuest tourGuest)
        {
            return _tourGuestRepository.Save(tourGuest);
        }
        public TourGuest UpdateConfirmation(int tourGuestId, bool confirmation)
        {
            return _tourGuestRepository.UpdateConfirmation(tourGuestId, confirmation);
        }
        public void Delete(TourGuest tourGuest)
        {
            _tourGuestRepository.Delete(tourGuest);
        }

        public TourGuest Update(TourGuest tourGuest)
        {
            return _tourGuestRepository.Update(tourGuest);
        }
    }
}
