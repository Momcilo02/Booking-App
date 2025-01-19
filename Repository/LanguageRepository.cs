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
    public class LanguageRepository : ILanguageRepository
    {
        private const string FilePath = "../../../Resources/Data/languages.csv";

        private readonly Serializer<Language> _serializer;

        private List<Language> _languages;
        public LanguageRepository()
        {
            _serializer = new Serializer<Language>();
            _languages = _serializer.FromCSV(FilePath);
        }
        public List<Language> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }
        public Language Get(int id)
        {
            List<Language> languages = GetAll();
            return languages.Find(l => l.Id == id);
        }
        public List<string> GetLanguageNames()
        {
            var cities = _languages.Select(a => a.Name).Distinct().ToList();
            return cities;
        }
        public int NextId()
        {
            _languages = _serializer.FromCSV(FilePath);
            if (_languages.Count < 1)
            {
                return 1;
            }
            return _languages.Max(x => x.Id) + 1;
        }
        public Language Save(Language language)
        {
            language.Id = NextId();
            _languages = _serializer.FromCSV(FilePath);
            _languages.Add(language);
            _serializer.ToCSV(FilePath, _languages);
            return language;
        }
    }
}
