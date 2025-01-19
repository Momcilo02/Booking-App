using BookingApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IAccommodationRenovationSuggestionRepository
    {
        List<AccommodationRenovationSuggestion> GetAll();
        AccommodationRenovationSuggestion Save(AccommodationRenovationSuggestion accommodationRenovationSuggestion);
        AccommodationRenovationSuggestion Delete(AccommodationRenovationSuggestion accommodationRenovationSuggestion);
        AccommodationRenovationSuggestion GetById(int id);
    }
}
