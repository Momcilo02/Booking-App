using BookingApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTO
{
    public class LanguageDTO
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public int Id { get; set; }
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                if (value != name)
                {
                    name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }
       
       
        public LanguageDTO()
        {

        }
        public LanguageDTO(Language language)
        {
            Id = language.Id;
            Name=language.Name;
        }
        public Language ToLanguage()
        {
            return new Language(Id,name);
        }
        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        }
    }
}
