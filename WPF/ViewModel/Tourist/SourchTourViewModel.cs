using BookingApp.Application.UseCases;
using BookingApp.Commands;
using BookingApp.Domain.Models;
using BookingApp.DTO;
using BookingApp.View.Tourist;
using BookingApp.WPF.View.Tourist;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModel.Tourist
{
    public class SourchTourViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public User User { get; set; }
        public ICommand SearchClick { get; }
        public ICommand ClearClick { get; }
        private readonly ComboBox CityInput;
        private readonly ComboBox CountryInput;
        private readonly ComboBox LanguageInput;
        private TextBox DurationInput;
        private TextBox NumGuestInput;
        public List<string> Cities { get; set; }
        public List<string> Countries { get; set; }
        public List<string> Languages { get; set; }
        public TourDTO SelectedTour { get; set; }
        public TourService TourService;
        private Frame frame;
        public Frame frame2 { get; set; }
        private LocationService LocationService { get; set; }
        private LanguageService LanguageService { get; set; }
        public ICommand ReserveClick { get; }
        public ICommand MenuClick { get; }
        private SearchTourReservationDTO _searchDTO;
        public SearchTourReservationDTO SearchDTO
        {
            get { return _searchDTO; }
            set
            {
                _searchDTO = value;
                OnPropertyChanged(nameof(SearchDTO));
            }
        }
        private ObservableCollection<TourDTO> _tours;
        public ObservableCollection<TourDTO> Tours
        {
            get { return _tours; }
            set
            {
                _tours = value;
                OnPropertyChanged(nameof(Tours));
            }
        }
        public SourchTourViewModel(User user, Frame fr, Frame fr2, ComboBox cityInput, ComboBox countryInput, TextBox durationInput, ComboBox languageInput, TextBox numGuestInput)
        {
            frame = fr;
            frame2 = fr2;
            User = user;
            CityInput = cityInput;
            CountryInput = countryInput;
            DurationInput = durationInput;
            LanguageInput = languageInput;
            NumGuestInput = numGuestInput;
            Cities = new List<string>();
            Countries = new List<string>();
            Languages = new List<string>();
            SelectedTour = new TourDTO();
            TourService = new TourService();
            LocationService = new LocationService();
            LanguageService = new LanguageService();
            SearchClick = new RelayCommand(param => Search(param));
            MenuClick = new RelayCommand(param => Menu(param));
            ClearClick = new RelayCommand(param => Clear(param));
            ReserveClick = new RelayCommand(param => Reserve(param));
            Tours = new ObservableCollection<TourDTO>();
            UpdateTours();
            GetCities();
            GetCountries();
            GetLanguages();
        }
        private void Search(object parameter)
        {
            UpdateTours();
            int numGuestsInput;
            int.TryParse(NumGuestInput.Text.Trim(), out numGuestsInput);
            var searchDTO = new SearchTourReservationDTO
            {
                CityInput = CityInput.Text?.Trim()?.ToLower(),
                CountryInput = CountryInput.Text?.Trim().ToLower(),
                DurationInput = DurationInput.Text?.Trim(),
                LanguageInput = LanguageInput.Text?.Trim()?.ToLower(),
                NumGuestsInput = numGuestsInput
            };
            Tours = ApplyFilters(Tours, searchDTO);
        }
        private void Menu(object parameter)
        {
            frame2.Content = null;
        }

        private void Clear(object parameter)
        {
            CityInput.Text = "";
            CountryInput.Text = "";
            DurationInput.Text = "";
            LanguageInput.Text = "";
            NumGuestInput.Text = "";
            UpdateTours();
            OnPropertyChanged(nameof(SearchDTO));
        }

        public void UpdateTours()
        {
            Tours.Clear();
            foreach (Tour tour in TourService.GetAll())
            {
                tour.Location = LocationService.Get(tour.Location.Id);
                tour.Language = LanguageService.Get(tour.Language.Id);
                Tours.Add(new TourDTO(tour));
            }
        }
        private ObservableCollection<TourDTO> ApplyFilters(ObservableCollection<TourDTO> tours, SearchTourReservationDTO searchDTO)
        {
            tours = TourService.FilterByCity(tours, searchDTO.CityInput);
            tours = TourService.FilterByCountry(tours, searchDTO.CountryInput);
            tours = TourService.FilterByDuration(tours, searchDTO.DurationInput);
            tours = TourService.FilterByLanguage(tours, searchDTO.LanguageInput);
            tours = TourService.FilterByGuests(tours, searchDTO.NumGuestsInput);
            return tours;
        }
        private void Reserve(object parameter)
        {
            if (SelectedTour == null)
            {
                MessageBox.Show("Please select a tour.");
                return;
            }
            if (SelectedTour.MaxGuests == 0)
            {
                MessageBox.Show("The tour is full.");
                return;
            }
            frame2.Navigate(new TourReservationView(SelectedTour, User, frame, frame2));

        }
        private void GetCities()
        {
            Cities.Clear();
            Cities.AddRange(LocationService.GetCities());
            CityInput.ItemsSource = Cities;
        }
        private void GetLanguages()
        {
            Languages.Clear();
            Languages.AddRange(LanguageService.GetLanguageNames());
            LanguageInput.ItemsSource = Languages;
        }
        private void GetCountries()
        {
            Countries.Clear();
            Countries.AddRange(LocationService.GetCountries());
            CountryInput.ItemsSource = Countries;
        }
        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
