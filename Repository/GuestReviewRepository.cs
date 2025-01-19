using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class GuestReviewRepository : IGuestReviewRepository
    {
        private const string FilePath = "../../../Resources/Data/review.csv";
        private readonly Serializer<GuestReview> _serializer;

        private List<GuestReview> _guestReviews;
        public GuestReviewRepository()
        {
            _serializer = new Serializer<GuestReview>();
            _guestReviews = _serializer.FromCSV(FilePath);

        }

        public List<GuestReview> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }
        public int NextId()
        {
            _guestReviews = _serializer.FromCSV(FilePath);
            if (_guestReviews.Count < 1)
            {
                return 1;
            }
            return _guestReviews.Max(x => x.Id) + 1;
        }
        public GuestReview Save(GuestReview guestReview)
        {
            guestReview.Id = NextId();
            _guestReviews = _serializer.FromCSV(FilePath);
            _guestReviews.Add(guestReview);
            _serializer.ToCSV(FilePath, _guestReviews);
            return guestReview;
        }
        public void Delete(GuestReview guestReview)
        {
            _guestReviews = _serializer.FromCSV(FilePath);
            GuestReview founded = _guestReviews.Find(ar => ar.Id == guestReview.Id);
            if (founded != null)
            {
                _guestReviews.Remove(founded);
            }
            _serializer.ToCSV(FilePath, _guestReviews);
        }
        public GuestReview Update(GuestReview guestReview)
        {
            _guestReviews = _serializer.FromCSV(FilePath);
            GuestReview current = _guestReviews.Find(ar => ar.Id == guestReview.Id);
            int index = _guestReviews.IndexOf(current);
            _guestReviews[index] = guestReview;
            _serializer.ToCSV(FilePath, _guestReviews);
            return guestReview;
        }

    }
}
