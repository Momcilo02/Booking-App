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
    public class LiveTourRepository : ILiveTourRepository
    {
        private const string FilePath = "../../../Resources/Data/live-tour.csv";
        private readonly Serializer<LiveTour> _serializer;

        private List<LiveTour> _liveTours;

        public LiveTourRepository()
        {
            _serializer = new Serializer<LiveTour>();
            _liveTours = _serializer.FromCSV(FilePath);
        }
        public List<LiveTour> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }
        public LiveTour Get(int id)
        {
            List<LiveTour> liveTours = GetAll();
            return liveTours.Find(l => l.Id == id);
        }
        public int NextId()
        {
            _liveTours = _serializer.FromCSV(FilePath);
            if (_liveTours.Count < 1)
            {
                return 1;
            }
            return _liveTours.Max(x => x.Id) + 1;
        }
        public LiveTour Save(LiveTour liveTour)
        {
            liveTour.Id = NextId();
            _liveTours = _serializer.FromCSV(FilePath);
            _liveTours.Add(liveTour);
            _serializer.ToCSV(FilePath, _liveTours);
            return liveTour;
        }
        public void Delete(LiveTour liveTour)
        {
            _liveTours = _serializer.FromCSV(FilePath);
            LiveTour founded = _liveTours.Find(ar => ar.Id == liveTour.Id);
            if (founded != null)
            {
                _liveTours.Remove(founded);
            }
            _serializer.ToCSV(FilePath, _liveTours);
        }
        public LiveTour Update(LiveTour liveTour)
        {
            _liveTours = _serializer.FromCSV(FilePath);
            LiveTour current = _liveTours.Find(ar => ar.Id == liveTour.Id);
            int index = _liveTours.IndexOf(current);
            _liveTours[index] = liveTour;
            _serializer.ToCSV(FilePath, _liveTours);
            return liveTour;
        }


    }
}
