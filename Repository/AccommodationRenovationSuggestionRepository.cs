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
    public class AccommodationRenovationSuggestionRepository : IAccommodationRenovationSuggestionRepository
    {
        private const string FilePath = "../../../Resources/Data/accommodationrenovationsuggestion.csv";
        private readonly Serializer<AccommodationRenovationSuggestion> _serializer;
        private List<AccommodationRenovationSuggestion> _suggestions;
        public AccommodationRenovationSuggestionRepository()
        {
            _serializer = new Serializer<AccommodationRenovationSuggestion>();
            _suggestions = _serializer.FromCSV(FilePath);
        }
        public AccommodationRenovationSuggestion Delete(AccommodationRenovationSuggestion accommodationRenovationSuggestion)
        {
            throw new NotImplementedException();
        }
        public int NextId()
        {
            _suggestions = _serializer.FromCSV(FilePath);
            if (_suggestions.Count < 1)
            {
                return 1;
            }
            return _suggestions.Max(x => x.Id) + 1;
        }
        public List<AccommodationRenovationSuggestion> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public AccommodationRenovationSuggestion GetById(int id)
        {
            throw new NotImplementedException();
        }
        public int GetReservationCountForAccommodationYear(int accommodationId)
        {
            return _suggestions.Count(r => r.Accommodation.Id == accommodationId);
        }
        public AccommodationRenovationSuggestion Save(AccommodationRenovationSuggestion accommodationRenovationSuggestion)
        {
            accommodationRenovationSuggestion.Id = NextId();
            _suggestions = _serializer.FromCSV(FilePath);
            _suggestions.Add(accommodationRenovationSuggestion);
            _serializer.ToCSV(FilePath, _suggestions);
            return accommodationRenovationSuggestion;
        }
    }
}
