using BookingApp.Application.Injector;
using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.DTO;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.UseCases
{
    public class TourReservationService
    {
        private ITourReservationRepository _tourReservationRepository;

        public TourReservationService()
        {
            _tourReservationRepository = Injector.Injector.CreateInstance<ITourReservationRepository>(); ;
        }
        public List<TourReservation> GetAll()
        {
            return _tourReservationRepository.GetAll();
        }
        public TourReservation Get(int id)
        {
            return _tourReservationRepository.Get(id);
        }
        public TourReservation Save(TourReservation tourReservation)
        {
            return _tourReservationRepository.Save(tourReservation);
        }


        public void Delete(TourReservation tourReservation)
        {
            _tourReservationRepository.Delete(tourReservation);
        }

        public TourReservation Update(TourReservation tourReservation)
        {
            return _tourReservationRepository.Update(tourReservation);
        }
        public ObservableCollection<TourReservationDTO> GetFinishedTours()
        {
            var finishedTours = new ObservableCollection<TourReservationDTO>();
            foreach (TourReservation tourReservation in _tourReservationRepository.GetAll())
            {
                if (tourReservation.IsActive == 0)
                {
                    finishedTours.Add(new TourReservationDTO(tourReservation));
                }
            }
            return finishedTours;
        }
    }
}
