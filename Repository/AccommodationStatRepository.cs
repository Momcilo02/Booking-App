using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Serializer;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class AccommodationStatRepository : IAccommodationStatsRepository
    {
        private const string FilePath = "../../../Resources/Data/accommodation_stats.csv";
        private readonly Serializer<AccommodationStats> _serializer;
        private List<AccommodationStats> _stats;
        public AccommodationStatRepository()
        {
            _serializer = new Serializer<AccommodationStats>();
            _stats = _serializer.FromCSV(FilePath);
        }

        public void Delete(AccommodationStats accommodation)
        {
            _stats = _serializer.FromCSV(FilePath);
            AccommodationStats founded = _stats.Find(ar => ar.Id == accommodation.Id);
            if (founded != null)
            {
                _stats.Remove(founded);
            }
            _serializer.ToCSV(FilePath, _stats);
        }

        public List<AccommodationStats> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public AccommodationStats GetById(int id)
        {
            throw new NotImplementedException();
        }
        public int NextId()
        {
            _stats = _serializer.FromCSV(FilePath);
            if (_stats.Count < 1)
            {
                return 1;
            }
            return _stats.Max(x => x.Id) + 1;
        }
        public AccommodationStats Save(AccommodationStats accommodation)
        {
            accommodation.Id = NextId();
            _stats = _serializer.FromCSV(FilePath);
            _stats.Add(accommodation);
            _serializer.ToCSV(FilePath, _stats);
            return accommodation;
        }

        public AccommodationStats Update(AccommodationStats accommodation)
        {
            _stats = _serializer.FromCSV(FilePath);
            AccommodationStats current = _stats.Find(ar => ar.Id == accommodation.Id);
            int index = _stats.IndexOf(current);
            _stats[index] = accommodation;
            _serializer.ToCSV(FilePath, _stats);
            return accommodation;
        }
    }
}
