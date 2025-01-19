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
    public class AccommodationReviewService
    {
        private readonly IAccommodationReviewRepository _accommodationReviewRepository;
        private readonly IAccommodationRepository _accommodationRepository;
        public AccommodationReviewService()
        {
            _accommodationReviewRepository = Injector.Injector.CreateInstance<IAccommodationReviewRepository>();
            _accommodationRepository = Injector.Injector.CreateInstance<IAccommodationRepository>();
        }
        public List<AccommodationReview> GetAll()
        {
            return _accommodationReviewRepository.GetAll();
        }

        public List<AccommodationReview> GetByUser(User user)
        {
            return _accommodationReviewRepository.GetByUser(user);
        }

        public AccommodationReview Save(AccommodationReview review)
        {
            return _accommodationReviewRepository.Save(review);
        }
        public AccommodationReview Delete(AccommodationReview review)
        {
            return _accommodationReviewRepository.Delete(review);
        }
        public AccommodationReview Update(AccommodationReview review)
        {
            return _accommodationReviewRepository.Update(review);
        }
        public AccommodationReview GetByUserAndAccommodation(User user, Accommodation accommodation)
        {
            List<AccommodationReview> accommodationReviews = GetByUser(user);
            foreach (var r in accommodationReviews)
            {
                if (r.Accommodation.Id == accommodation.Id)
                {
                    return r;
                }
            }
            return new AccommodationReview() { Id = -1 };
        }
        public AccommodationReview Get(int id) 
        {
            return _accommodationReviewRepository.Get(id);
        }
        public double CalculateAvgGradeForOwner(int id) 
        {
            List<Accommodation> accommodations = _accommodationRepository.GetAccommodationsByOwnerId(id);
            List<AccommodationReview> reviws = new List<AccommodationReview>();
            foreach (var r in GetAll()) 
            {
                r.Accommodation = _accommodationRepository.GetAccommodation(r.Id);
                if (accommodations.Contains(r.Accommodation))
                { 
                    reviws.Add(r);
                }
            }
            double total = 0;
            double counter = 0;
            foreach (var r in reviws)
            {
                total += (r.Cleanness + r.Correctness) / 2;
                counter++;
            }
            return total/counter;
        }
        public List<AccommodationReview> GetReviewsForOwner(int id) 
        {
            List<Accommodation> accommodations = _accommodationRepository.GetAccommodationsByOwnerId(id);
            List<AccommodationReview> reviws = new List<AccommodationReview>();
            foreach (var r in GetAll())
            {
                r.Accommodation = _accommodationRepository.GetAccommodation(r.Id);
                if (accommodations.Contains(r.Accommodation))
                {
                    reviws.Add(r);
                }
            }
            return reviws;
        }

    }
}
