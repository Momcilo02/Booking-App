using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTO
{
    public class SearchTourReservationDTO : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private string cityInput;
        public string CityInput
        {
            get { return cityInput; }
            set
            {
                if (cityInput != value)
                {
                    cityInput = value;
                    OnPropertyChanged(nameof(CityInput));
                }
            }
        }
        private string countryInput;
        public string CountryInput
        {
            get { return countryInput; }
            set
            {
                if (countryInput != value)
                {
                    countryInput = value;
                    OnPropertyChanged(nameof(CountryInput));
                }
            }
        }

        private string durationInput;
        public string DurationInput
        {
            get { return durationInput; }
            set
            {
                if (durationInput != value)
                {
                    durationInput = value;
                    OnPropertyChanged(nameof(DurationInput));
                }
            }
        }

        private string languageInput;
        public string LanguageInput
        {
            get { return languageInput; }
            set
            {
                if (languageInput != value)
                {
                    languageInput = value;
                    OnPropertyChanged(nameof(LanguageInput));
                }
            }
        }

        private int numGuestsInput;
        public int NumGuestsInput
        {
            get { return numGuestsInput; }
            set
            {
                if (numGuestsInput != value)
                {
                    numGuestsInput = value;
                    OnPropertyChanged(nameof(NumGuestsInput));
                }
            }
        }
        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        }
    }
}
