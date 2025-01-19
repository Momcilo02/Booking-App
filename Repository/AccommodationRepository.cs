using BookingApp.Application.Injector;
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
    public class AccommodationRepository: IAccommodationRepository
    {
        private const string FilePath = "../../../Resources/Data/accommodation.csv";
        private readonly Serializer<Accommodation> _serializer;

        private List<Accommodation> _accommodations;
        private LocationRepository _locationRepository;
        private List<Location> _locations;
        private UserRepository _userRepository;
        public AccommodationRepository()
        {
            _serializer = new Serializer<Accommodation>();
            _accommodations = _serializer.FromCSV(FilePath);
            _locationRepository = new LocationRepository();
            _locations = _locationRepository.GetAll();
            _userRepository = new UserRepository();
            FindLocations();
        }
        private void Initialize()
        {
            _accommodations = _serializer.FromCSV(FilePath);
            _locations = _locationRepository.GetAll();
            foreach (Accommodation accommodation in _accommodations)
            {
                accommodation.Location = _locationRepository.Get(accommodation.Location.Id);
                accommodation.Owner = _userRepository.Get(accommodation.Owner.Id);
            }
        }
        private void FindLocations()
        {
            foreach (Accommodation accommodation in _accommodations)
            {
                accommodation.Location = _locationRepository.Get(accommodation.Location.Id);
            }
        }
        public List<Accommodation> GetAll()
        {
            _accommodations.Clear();
            _locations.Clear();
            Initialize();
            return _accommodations;
        }
        public int NextId()
        {
            _accommodations = _serializer.FromCSV(FilePath);
            if (_accommodations.Count < 1)
            {
                return 1;
            }
            return _accommodations.Max(x => x.Id) + 1;
        }
        public Accommodation Save(Accommodation accommodetion)
        {
            accommodetion.Id = NextId();
            _accommodations = _serializer.FromCSV(FilePath);
            _accommodations.Add(accommodetion);
            _serializer.ToCSV(FilePath, _accommodations);
            return accommodetion;
        }
        public void Delete(Accommodation accommodation)
        {
            _accommodations = _serializer.FromCSV(FilePath);
            Accommodation founded = _accommodations.Find(ar => ar.Id == accommodation.Id);
            if (founded != null)
            {
                _accommodations.Remove(founded);
            }
            _serializer.ToCSV(FilePath, _accommodations);
        }
        public Accommodation Update(Accommodation accommodation)
        {
            _accommodations = _serializer.FromCSV(FilePath);
            Accommodation current = _accommodations.Find(ar => ar.Id == accommodation.Id);
            int index = _accommodations.IndexOf(current);
            _accommodations[index] = accommodation;
            _serializer.ToCSV(FilePath, _accommodations);
            return accommodation;
        }
        public List<Accommodation> GetAccommodationsByOwnerId(int id)
        {
            var accommodationsForOwner = _accommodations.Where(a => a.Id == id).ToList();
            return accommodationsForOwner;
        }
        public Accommodation GetAccommodation(int id)
        {
            _accommodations.Clear();
            _locations.Clear();
            Initialize();
            Accommodation accommodation = _accommodations.Find(a => a.Id == id);
            return accommodation;
        }

        public Accommodation GetById(int id)
        {
            _accommodations.Clear();
            _locations.Clear();
            Initialize();
            return _accommodations.Find(a => a.Id == id);
        }
    }
}
