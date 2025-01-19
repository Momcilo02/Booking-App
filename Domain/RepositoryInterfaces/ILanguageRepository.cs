using BookingApp.Domain.Models;
using System.Collections.Generic;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface ILanguageRepository
    {
        List<Language> GetAll();
        Language Get(int id);
        List<string> GetLanguageNames();
        
        int NextId();
        Language Save(Language language);
    }
}
