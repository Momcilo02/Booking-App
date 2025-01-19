using BookingApp.DTO;
using BookingApp.DTO.TourDTOs;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.UseCases
{
    public class StatisticsService
    {
        private readonly TourRepository _tourRepository;
        private readonly LiveTourRepository _liveTourRepository;
        private readonly TourReservationRepository _tourReservationRepository;
        private readonly TourGuestRepository _tourGuestRepository;
        private readonly TourTimeRepository _tourTimeRepository;
        private readonly LocationRepository _locationRepository;

        public StatisticsService()
        {
            _tourRepository = new TourRepository();
            _tourReservationRepository = new TourReservationRepository();
            _liveTourRepository = new LiveTourRepository();
            _tourGuestRepository = new TourGuestRepository();
            _tourTimeRepository = new TourTimeRepository();
            _locationRepository = new LocationRepository();
        }

        public TourStatisticsDto GetStatisticsForMostVisitedTour(int? tourId, int? year)
        {
            var tours = _tourRepository.GetAll();
            var tourStatistics = new TourStatisticsDto();



            var allLiveTuours = _liveTourRepository.GetAll();
            var allTourTimes = _tourTimeRepository.GetAll();

            var allLocations = _locationRepository.GetAll();

            var tourEndedLiveTours = allLiveTuours.Where(x => (tourId == null || x.TourId == tourId) && x.IsFinished == 1);

            if (!tourEndedLiveTours.Any())
            {
                return tourStatistics;
            }

            var tourEndedLiveTourIds = tourEndedLiveTours.Select(x => x.Id).ToList();

            var allReservations = _tourReservationRepository.GetAll();

            var allTourGuests = _tourGuestRepository.GetAll();


            Dictionary<int, List<int>> numOfTourGuestPerEndedTour = new Dictionary<int, List<int>>();
            foreach (int endedTourId in tourEndedLiveTourIds)
            {
                numOfTourGuestPerEndedTour.Add(endedTourId, new List<int>() { 0, 0, 0 });
                var tourReservations = allReservations.Where(x => tourEndedLiveTourIds.Contains(x.LiveTourId)).ToList();
                var tourReservationIds = tourReservations.Select(x => x.Id).ToList();

                foreach (int reservationId in tourReservationIds)
                {
                    var reservation = tourReservations.SingleOrDefault(x => x.Id == reservationId);

                    var dateTime = allTourTimes.SingleOrDefault(x => x.Id == reservation.TourTimeId);

                    if (year != null && year != dateTime.Time.Year)
                    {
                        continue;
                    }

                    var tourGuests = allTourGuests.Where(x => tourReservationIds.Contains(x.TourReservationId)).ToList();

                    foreach (var tourGuest in tourGuests)
                    {
                        if (tourGuest.Age < 18)
                        {
                            numOfTourGuestPerEndedTour[endedTourId][0]++;
                        }
                        else if (tourGuest.Age < 50)
                        {
                            numOfTourGuestPerEndedTour[endedTourId][1]++;

                        }
                        else
                        {
                            numOfTourGuestPerEndedTour[endedTourId][2]++;

                        }
                    }
                }
            }

            var mostVisitedTourId = numOfTourGuestPerEndedTour.OrderByDescending(pair => pair.Value.Sum()).FirstOrDefault().Key;
            var mostVisitedTour = tourEndedLiveTours.SingleOrDefault(x => x.Id == mostVisitedTourId);

            if (tourId == null)
            {

                tourId = mostVisitedTour.TourId;
            }

            var tour = tours.SingleOrDefault(x => x.Id == tourId);
            if (tour == null)
            {
                return tourStatistics;
            }

            var tourTime = allTourTimes.SingleOrDefault(x => x.Id == mostVisitedTour.TourTimeId);

            var location = allLocations.SingleOrDefault(x => x.Id == tour.Id);

            tourStatistics.Id = tour.Id;
            tourStatistics.Name = tour.Name;
            tourStatistics.TotalNumerOfTourists = numOfTourGuestPerEndedTour[mostVisitedTourId].Sum();
            tourStatistics.Under18Num = numOfTourGuestPerEndedTour[mostVisitedTourId][0];
            tourStatistics.From18To50 = numOfTourGuestPerEndedTour[mostVisitedTourId][1];
            tourStatistics.Above50 = numOfTourGuestPerEndedTour[mostVisitedTourId][2];
            tourStatistics.Date = tourTime.Time;
            tourStatistics.TourImage = tour.ImagePaths.First();


            tourStatistics.LocationDto = new LocationDTO
            {
                Id = location.Id,
                City = location.City,
                Country = location.Country,
                FullLocation = location.City + location.Country
            };

            return tourStatistics;
        }

    }
}
