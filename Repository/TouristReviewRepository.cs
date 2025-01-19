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
    public class TouristReviewRepository : ITouristReviewRepository
    {
        private const string FilePath = "../../../Resources/Data/tourist-review.csv";

        private readonly Serializer<TouristReview> _serializer;

        private List<TouristReview> _touristreviews;

        public TouristReviewRepository()
        {
            _serializer = new Serializer<TouristReview>();
            _touristreviews = _serializer.FromCSV(FilePath);
        }

        public List<TouristReview> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }
        public TouristReview Get(int id)
        {
            List<TouristReview> vouchers = GetAll();
            return vouchers.Find(l => l.Id == id);
        }
        public TouristReview Save(TouristReview touristreview)
        {
            touristreview.Id = NextId();
            _touristreviews = _serializer.FromCSV(FilePath);
            _touristreviews.Add(touristreview);
            _serializer.ToCSV(FilePath, _touristreviews);
            return touristreview;
        }

        public int NextId()
        {
            _touristreviews = _serializer.FromCSV(FilePath);
            if (_touristreviews.Count < 1)
            {
                return 1;
            }
            return _touristreviews.Max(c => c.Id) + 1;
        }

        public void Delete(TouristReview touristreview)
        {
            _touristreviews = _serializer.FromCSV(FilePath);
            TouristReview founded = _touristreviews.Find(c => c.Id == touristreview.Id);
            if (founded != null)
            {
                _touristreviews.Remove(founded);
                _serializer.ToCSV(FilePath, _touristreviews);
            }
        }

        public TouristReview Update(TouristReview touristReview)
        {
            _touristreviews = _serializer.FromCSV(FilePath);
            TouristReview current = _touristreviews.Find(ar => ar.Id == touristReview.Id);
            int index = _touristreviews.IndexOf(current);
            _touristreviews[index] = touristReview;
            _serializer.ToCSV(FilePath, _touristreviews);
            return touristReview;
        }

    }
}
