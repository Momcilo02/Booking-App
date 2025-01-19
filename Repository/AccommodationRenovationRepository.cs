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
    public class AccommodationRenovationRepository : IAccommodationRenovationRepository
    {
        private const string FilePath = "../../../Resources/Data/renovations.csv";
        private readonly Serializer<AccommodationRenovation> _serializer;
        private List<AccommodationRenovation> _renovations;
        public AccommodationRenovationRepository()
        {
            _serializer = new Serializer<AccommodationRenovation>();
            _renovations = _serializer.FromCSV(FilePath);
        }
        public void Delete(AccommodationRenovation accommodation)
        {
            _renovations = _serializer.FromCSV(FilePath);
            AccommodationRenovation founded = _renovations.Find(ar => ar.Id == accommodation.Id);
            if (founded != null)
            {
                _renovations.Remove(founded);
            }
            _serializer.ToCSV(FilePath, _renovations);
        }

        public List<AccommodationRenovation> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public AccommodationRenovation GetById(int id)
        {
            throw new NotImplementedException();
        }
        public int NextId()
        {
            _renovations = _serializer.FromCSV(FilePath);
            if (_renovations.Count < 1)
            {
                return 1;
            }
            return _renovations.Max(x => x.Id) + 1;
        }

        public AccommodationRenovation Save(AccommodationRenovation accommodation)
        {
            accommodation.Id = NextId();
            _renovations = _serializer.FromCSV(FilePath);
            _renovations.Add(accommodation);
            _serializer.ToCSV(FilePath, _renovations);
            return accommodation;
        }

        public AccommodationRenovation Update(AccommodationRenovation accommodation)
        {
            _renovations = _serializer.FromCSV(FilePath);
            AccommodationRenovation current = _renovations.Find(ar => ar.Id == accommodation.Id);
            int index = _renovations.IndexOf(current);
            _renovations[index] = accommodation;
            _serializer.ToCSV(FilePath, _renovations);
            return accommodation;
        }
    }
}
