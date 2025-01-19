using BookingApp.Application.Injector;
using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.DTO;
using BookingApp.Repository;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.UseCases
{
    public class TourService
    {
        private ITourRepository _tourRepository;
        private TourRequestService _tourRequestRepository;
        private TourReservationService _tourReservationRepository;
        public TourService()
        {
            _tourRepository = Injector.Injector.CreateInstance<ITourRepository>();
            _tourRequestRepository = new TourRequestService();
            _tourReservationRepository = new TourReservationService();
        }
        public List<Tour> GetAll()
        {
            return _tourRepository.GetAll();
        }
        public List<Tour> GetReservedTours()
        {
            List<Tour> tour=new List<Tour>();
            foreach(TourReservation tourReservation in _tourReservationRepository.GetAll())
            {
                foreach(Tour tours in _tourRepository.GetAll())
                {
                    if (tourReservation.TourId == tours.Id)
                    {
                        tour.Add(tours);
                    }
                }
            }
            return tour;
        }

        public List<Tour> GetGuideTours(int guideId)
        {
            return _tourRepository.GetAll().Where(x => x.GuideId == guideId).ToList();
        }
        public List<Tour> GetByName(string name)
        {
            return _tourRepository.GetAll().Where(x => x.Name == name).ToList();
        }
        public Tour Get(int id)
        {
            return _tourRepository.Get(id);
        }
        public Tour Save(Tour tour)
        {
            return _tourRepository.Save(tour);
        }

        public Tour Update(Tour tour)
        {
            return _tourRepository.Update(tour);
        }
        public Tour UpdateCapacity(int tourId, int maxGuests)
        {
            return _tourRepository.UpdateCapacity(tourId, maxGuests);
        }
        public void Delete(Tour tour)
        {
            _tourRepository.Delete(tour);
        }
        public ObservableCollection<TourDTO> FilterByCity(ObservableCollection<TourDTO> tours, string cityInput)
        {

            if (string.IsNullOrEmpty(cityInput))
                return tours;

            return new ObservableCollection<TourDTO>(tours.Where(tour => MatchesCity(tour, cityInput)));

        }
        public ObservableCollection<TourDTO> FilterByCountry(ObservableCollection<TourDTO> tours, string countryInput)
        {

            if (string.IsNullOrEmpty(countryInput))
                return tours;

            return new ObservableCollection<TourDTO>(tours.Where(tour => MatchesCountry(tour, countryInput)));

        }

        public ObservableCollection<TourDTO> FilterByDuration(ObservableCollection<TourDTO> tours, string durationInput)
        {
            if (string.IsNullOrEmpty(durationInput))
                return tours;

            return new ObservableCollection<TourDTO>(tours.Where(tour => tour.Duration.ToString() == durationInput));
        }

        public ObservableCollection<TourDTO> FilterByLanguage(ObservableCollection<TourDTO> tours, string languageInput)
        {
            if (string.IsNullOrEmpty(languageInput))
                return tours;

            return new ObservableCollection<TourDTO>(tours.Where(tour => tour.Language.Name.Trim().ToLower()==languageInput));
        }

        public ObservableCollection<TourDTO> FilterByGuests(ObservableCollection<TourDTO> tours, int numGuestsInput)
        {
            if (numGuestsInput <= 0)
                return tours;

            return new ObservableCollection<TourDTO>(tours.Where(tour => numGuestsInput <= tour.MaxGuests));
        }
        public bool MatchesCity(TourDTO tour, string cityInput)
        {
            if (string.IsNullOrEmpty(cityInput))
                return true;
            if (tour.Location == null)
                return false;
            string tourCity = tour.Location.City.Trim().ToLower();
            return tourCity == cityInput;
        }
        public bool MatchesCountry(TourDTO tour, string countryInput)
        {
            if (string.IsNullOrEmpty(countryInput))
                return true;
            if (tour.Location == null)
                return false;
            string tourCity = tour.Location.Country.Trim().ToLower();
            return tourCity == countryInput;
        }
        public ObservableCollection<Tour> GetToursByLanguage()
        {
            var rejectedLanguages = _tourRequestRepository.GetRejectedTours().Select(tr => tr.Language.Id).ToHashSet();
            ObservableCollection<Tour> ToursByLanguage = new ObservableCollection<Tour>(
                _tourRepository.GetAll()
                    .Where(tour => rejectedLanguages.Contains(tour.Language.Id))
                    .DistinctBy(tour => tour.Language.Id)
            );
            return ToursByLanguage;
        }
        public ObservableCollection<Tour> GetToursByLocation()
        {
            var rejectedLocations = _tourRequestRepository.GetRejectedTours().Select(tr => tr.Location.Id).ToHashSet();
            ObservableCollection<Tour> ToursByLocation = new ObservableCollection<Tour>(
                _tourRepository.GetAll()
                    .Where(tour => rejectedLocations.Contains(tour.Location.Id))
                    .DistinctBy(tour => tour.Language.Id)
            );
            return ToursByLocation;
        }

    }
}
