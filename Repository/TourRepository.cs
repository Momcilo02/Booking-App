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
    public class TourRepository : ITourRepository
    {
        private const string FilePath = "../../../Resources/Data/tour.csv";
        private readonly Serializer<Tour> _serializer;

        private List<Tour> _tours;

        public TourRepository()
        {
            _serializer = new Serializer<Tour>();
            _tours = _serializer.FromCSV(FilePath);
        }
        public List<Tour> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }
        public Tour Get(int id)
        {
            List<Tour> tours = GetAll();
            return tours.Find(l => l.Id == id);
        }
        public int NextId()
        {
            _tours = _serializer.FromCSV(FilePath);
            if (_tours.Count < 1)
            {
                return 1;
            }
            return _tours.Max(x => x.Id) + 1;
        }
        public Tour Save(Tour tour)
        {
            tour.Id = NextId();
            _tours = _serializer.FromCSV(FilePath);
            _tours.Add(tour);
            _serializer.ToCSV(FilePath, _tours);
            return tour;
        }
        public void Delete(Tour tour)
        {
            _tours = _serializer.FromCSV(FilePath);
            Tour founded = _tours.Find(ar => ar.Id == tour.Id);
            if (founded != null)
            {
                _tours.Remove(founded);
            }
            _serializer.ToCSV(FilePath, _tours);
        }
        public Tour Update(Tour tour)
        {
            _tours = _serializer.FromCSV(FilePath);
            Tour current = _tours.Find(ar => ar.Id == tour.Id);
            int index = _tours.IndexOf(current);
            _tours[index] = tour;
            _serializer.ToCSV(FilePath, _tours);
            return tour;
        }
        public Tour UpdateCapacity(int tourId, int maxGuests)
        {
            _tours = _serializer.FromCSV(FilePath);
            Tour current = _tours.Find(ar => ar.Id == tourId);

            if (current != null)
            {
                current.MaxGuests = maxGuests;
                int index = _tours.IndexOf(current);
                _tours[index] = current;
                _serializer.ToCSV(FilePath, _tours);
            }

            return current;
        }

        // public List<Tour> GetByUser(User user)
        //{
        //   _tours = _serializer.FromCSV(FilePath);
        // return _tours.FindAll(ar => ar.User.Id == user.Id);
        //}
    }
}
