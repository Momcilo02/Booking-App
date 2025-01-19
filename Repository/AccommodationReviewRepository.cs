using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Serializer;

namespace BookingApp.Repository
{
    public class AccommodationReviewRepository:IAccommodationReviewRepository
    {
        private const string FilePath = "../../../Resources/Data/accommodationreview.csv";
        private readonly Serializer<AccommodationReview> _serializer;
        private List<AccommodationReview> _accommodationReviews;

        public AccommodationReviewRepository()
        {
            _serializer = new Serializer<AccommodationReview>();
            _accommodationReviews = _serializer.FromCSV(FilePath);
        }
        public List<AccommodationReview> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }
        public int NextId()
        {
            _accommodationReviews = _serializer.FromCSV(FilePath);
            if (_accommodationReviews.Count < 1)
            {
                return 1;
            }
            return _accommodationReviews.Max(x => x.Id) + 1;
        }
        public AccommodationReview Save(AccommodationReview review)
        {
            review.Id = NextId();
            _accommodationReviews = GetAll();
            _accommodationReviews.Add(review);
            _serializer.ToCSV(FilePath, _accommodationReviews);
            return review;
        }

        public AccommodationReview Delete(AccommodationReview review)
        {
            _accommodationReviews = GetAll();
            AccommodationReview founded = _accommodationReviews.Find(r => r.Id == review.Id);
            if (founded != null)
            {
                _accommodationReviews.Remove(founded);
            }
            _serializer.ToCSV(FilePath, _accommodationReviews);
            return review;
        }
        public AccommodationReview Update(AccommodationReview review)
        {
            _accommodationReviews = GetAll();
            AccommodationReview current = _accommodationReviews.Find(r => r.Id == review.Id);
            int index = _accommodationReviews.IndexOf(current);
            _accommodationReviews[index] = review;
            _serializer.ToCSV(FilePath, _accommodationReviews);
            return review;
        }

        public List<AccommodationReview> GetByUser(User user)
        {
            _accommodationReviews = GetAll();
            return _accommodationReviews.FindAll(r => r.Guest.Id == user.Id);
        }
        public AccommodationReview Get(int id)
        {
            AccommodationReview accommodation = _accommodationReviews.Find(a => a.Id == id);
            return accommodation;
        }

    }
}
