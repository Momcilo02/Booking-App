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
    public class TourTimeRepository : ITourTimeRepository
    {
        private const string FilePath = "../../../Resources/Data/tour-time.csv";
        private readonly Serializer<TourTime> _serializer;

        private List<TourTime> _tourTimes;

        public TourTimeRepository()
        {
            _serializer = new Serializer<TourTime>();
            _tourTimes = _serializer.FromCSV(FilePath);
        }
        public List<TourTime> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }
        public TourTime Get(int id)
        {
            List<TourTime> tourTimes = GetAll();
            return tourTimes.Find(l => l.Id == id);
        }
        public int NextId()
        {
            _tourTimes = _serializer.FromCSV(FilePath);
            if (_tourTimes.Count < 1)
            {
                return 1;
            }
            return _tourTimes.Max(x => x.Id) + 1;
        }
        public TourTime Save(TourTime tourTime)
        {
            tourTime.Id = NextId();
            _tourTimes = _serializer.FromCSV(FilePath);
            _tourTimes.Add(tourTime);
            _serializer.ToCSV(FilePath, _tourTimes);
            return tourTime;
        }
        public void Delete(TourTime tourTime)
        {
            _tourTimes = _serializer.FromCSV(FilePath);
            TourTime founded = _tourTimes.Find(ar => ar.Id == tourTime.Id);
            if (founded != null)
            {
                _tourTimes.Remove(founded);
            }
            _serializer.ToCSV(FilePath, _tourTimes);
        }

        public void DeleteById(int id)
        {
            _tourTimes = _serializer.FromCSV(FilePath);
            TourTime founded = _tourTimes.Find(ar => ar.Id == id);
            if (founded != null)
            {
                _tourTimes.Remove(founded);
            }
            _serializer.ToCSV(FilePath, _tourTimes);
        }
        public TourTime Update(TourTime tourTime)
        {
            _tourTimes = _serializer.FromCSV(FilePath);
            TourTime current = _tourTimes.Find(ar => ar.Id == tourTime.Id);
            int index = _tourTimes.IndexOf(current);
            _tourTimes[index] = tourTime;
            _serializer.ToCSV(FilePath, _tourTimes);
            return tourTime;
        }
        public List<TourTime> GetByTourId(int tourId)
        {
            _tourTimes = _serializer.FromCSV(FilePath);
            return _tourTimes.FindAll(t => t.TourId == tourId);
        }
    }
}
