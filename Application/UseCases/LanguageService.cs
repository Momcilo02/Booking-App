using BookingApp.Application.Injector;
using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.UseCases
{
    public class LanguageService
    {
        private readonly ILanguageRepository _languageRepository;
        public LanguageService()
        {
            _languageRepository = Injector.Injector.CreateInstance<ILanguageRepository>();
        }
        public List<Language> GetAll()
        {
            return _languageRepository.GetAll();
        }

        public Language Save(Language language)
        {
            return _languageRepository.Save(language);
        }

        public Language Get(int id)
        {
            return _languageRepository.Get(id);
        }
        public string GetLanguageName (int id)
        {
            Language language= _languageRepository.Get(id);
            string name = language.Name;
            return name;
        }
        public List<string> GetLanguageNames()
        {
            return _languageRepository.GetLanguageNames();
        }
        public ObservableCollection<LanguageDTO> InitializeLanguages()
        {
            var languages = new ObservableCollection<LanguageDTO>();

            foreach (Language language in GetAll())
            {
                languages.Add(new LanguageDTO(language));
            }

            return languages;
        }
    }
}
