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
    public class TourGuestRepository : ITourGuestRepository
    {
        private const string FilePath = "../../../Resources/Data/tour-guest.csv";
        private readonly Serializer<TourGuest> _serializer;

        private List<TourGuest> _tourguests;

        public TourGuestRepository()
        {
            _serializer = new Serializer<TourGuest>();
            _tourguests = _serializer.FromCSV(FilePath);
        }
        public List<TourGuest> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }
        public int NextId()
        {
            _tourguests = _serializer.FromCSV(FilePath);
            if (_tourguests.Count < 1)
            {
                return 1;
            }
            return _tourguests.Max(x => x.Id) + 1;
        }
        public TourGuest Save(TourGuest tour)
        {
            tour.Id = NextId();
            _tourguests = _serializer.FromCSV(FilePath);
            _tourguests.Add(tour);
            _serializer.ToCSV(FilePath, _tourguests);
            return tour;
        }
        public TourGuest UpdateConfirmation(int tourGuestId, bool confirmation)
        {
            _tourguests = _serializer.FromCSV(FilePath);
            TourGuest current = _tourguests.Find(ar => ar.Id == tourGuestId);

            if (current != null)
            {
                current.Confirmation = confirmation;
                int index = _tourguests.IndexOf(current);
                _tourguests[index] = current;
                _serializer.ToCSV(FilePath, _tourguests);
            }

            return current;
        }


        public TourGuest Update(TourGuest tourGuest)
        {
            _tourguests = _serializer.FromCSV(FilePath);
            TourGuest current = _tourguests.Find(ar => ar.Id == tourGuest.Id);
            int index = _tourguests.IndexOf(current);
            _tourguests[index] = tourGuest;
            _serializer.ToCSV(FilePath, _tourguests);
            return tourGuest;
        }
        public void Delete(TourGuest tour)
        {
            _tourguests = _serializer.FromCSV(FilePath);
            TourGuest founded = _tourguests.Find(ar => ar.Id == tour.Id);
            if (founded != null)
            {
                _tourguests.Remove(founded);
            }
            _serializer.ToCSV(FilePath, _tourguests);
        }


    }
}
