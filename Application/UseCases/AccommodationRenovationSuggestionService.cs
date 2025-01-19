using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.UseCases
{
    public class AccommodationRenovationSuggestionService
    {
        private readonly IAccommodationRenovationSuggestionRepository accommodationRenovationSuggestionRepository;
        public AccommodationRenovationSuggestionService()
        {
            accommodationRenovationSuggestionRepository = Injector.Injector.CreateInstance<IAccommodationRenovationSuggestionRepository>();
        }
        public List<AccommodationRenovationSuggestion> GetAll()
        {
            return accommodationRenovationSuggestionRepository.GetAll();
        }
        public AccommodationRenovationSuggestion Save(AccommodationRenovationSuggestion suggestion)
        {
            return accommodationRenovationSuggestionRepository.Save(suggestion);
        }
    }
}
