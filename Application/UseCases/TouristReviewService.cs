using BookingApp.Application.Injector;
using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.DTO.TourReviewDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.UseCases
{
    public class TouristReviewService
    {
        private ITouristReviewRepository _tourReviewRepository;
        private readonly TourReservationService _tourReservationService;
        private readonly CheckPointService _checkPointService;
        private readonly TourService _tourService;
        private readonly LiveTourService _liveTourService;
        public TouristReviewService()
        {
            _tourReviewRepository = Injector.Injector.CreateInstance<ITouristReviewRepository>();
            _tourReservationService = new TourReservationService();
            _checkPointService = new CheckPointService();
            _tourService = new TourService();
            _liveTourService = new LiveTourService();
        }
        public List<TouristReview> GetAll()
        {
            return _tourReviewRepository.GetAll();
        }


        public void ReportReview(int id) 
        {
            var review = _tourReviewRepository.Get(id);
            review.IsValid = -1;
            _tourReviewRepository.Update(review);
        }

        public List<TourReviewListItem> GetReviewsForEndedTour(int liveTourId)
        {
            var tourReviewListItems = new List<TourReviewListItem>();

            var allEndedTours = _liveTourService.GetAll().Where(x => x.IsFinished == 1).ToList();
            var allTours = _tourService.GetAll();
            var allReviews = _tourReviewRepository.GetAll();
            var allCheckPoints = _checkPointService.GetAll();

            var allReservations = _tourReservationService.GetAll();
            var allReservationIds = allReservations.Select(x => x.Id).ToList();

            var tourReviews = allReviews.Where(x => allReservationIds.Contains(x.TourReservationId)).ToList();

            foreach (var tourReview in tourReviews)
            {
                var reservation = allReservations.SingleOrDefault(x => x.Id == tourReview.TourReservationId);
                if (reservation == null) continue;

                var checkPoint = allCheckPoints.SingleOrDefault(x => x.Id == reservation.CheckPointId);
                if (checkPoint == null) continue;

                var endedTour = allEndedTours.SingleOrDefault(x => x.Id == liveTourId);
                if (endedTour == null) continue;

                var tour = allTours.SingleOrDefault(x => x.Id == endedTour.TourId);
                if (tour == null) continue;



                var tourReviewListItem = new TourReviewListItem
                {
                    Id = tourReview.Id,
                    Name = tour.Name,
                    CheckPoint = checkPoint.Name,
                    TourInterest = tourReview.TourInterest,
                    GuideKnowledge = tourReview.GuideKnowledge,
                    GuideLanguage = tourReview.GuideLanguage,
                    Comment = tourReview.Comment,
                    IsValid = tourReview.IsValid == 1 ? true : false
                };

                tourReviewListItems.Add(tourReviewListItem);
            }

            return tourReviewListItems;


        }
        public TouristReview Save(TouristReview touristReview)
        {
            return _tourReviewRepository.Save(touristReview);
        }

        public TouristReview Get(int id)
        {
            return _tourReviewRepository.Get(id);
        }
        public void Delete(TouristReview touristReview)
        {
            _tourReviewRepository.Delete(touristReview);
        }
    }
}
