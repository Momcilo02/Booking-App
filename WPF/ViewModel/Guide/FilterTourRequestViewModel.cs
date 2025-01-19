using BookingApp.Application.UseCases;
using BookingApp.Commands;
using BookingApp.Domain.Models;
using BookingApp.DTO;
using BookingApp.WPF.View.Guide;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModel.Guide
{
    public class FilterTourRequestViewModel : INotifyPropertyChanged
    {
        private LocationService _locationService;
        private LanguageService _languageService;
        private Location _selectedCityLocation;
        private Location _selectedStateLocation;
        private Language _selectedLanguage;

        public Location SelectedCityLocation
        {
            get => _selectedCityLocation;
            set
            {
                if (_selectedCityLocation != value)
                {
                    _selectedCityLocation = value;
                    FilterOptions.SelectedCity = _selectedCityLocation.City;
                    OnPropertyChanged(nameof(SelectedCityLocation));
                }
            }
        }
        public Location SelectedStateLocation
        {
            get => _selectedStateLocation;
            set
            {
                if (_selectedStateLocation != value)
                {
                    _selectedStateLocation = value;
                    FilterOptions.SelectedCountry = _selectedStateLocation.City;
                    OnPropertyChanged(nameof(SelectedStateLocation));
                }
            }
        }

        public ObservableCollection<Location> AvailableLocations { get; set; }
        public ObservableCollection<Language> AvailableLanguages { get; set; }

        public ICommand ConfirmCommand { get; private set; }
        public TourRequestFilterOptions FilterOptions { get; private set; }

        public Frame MainFrame { get; set; }
        public User User{ get; set; }
        public bool ForComplex { get; set; }

        public FilterTourRequestViewModel(Frame mainFrame, User user, bool forComplex)
        {
            MainFrame = mainFrame;
            ForComplex = forComplex;
            _locationService = new LocationService();
            _languageService = new LanguageService();

            AvailableLocations = new ObservableCollection<Location>(_locationService.GetAll());
            AvailableLanguages = new ObservableCollection<Language>(_languageService.GetAll());

            FilterOptions = new TourRequestFilterOptions();
            ConfirmCommand = new RelayCommand(ApplyFilters);
            User = user;
        }

        private void ApplyFilters(object obj)
        {
            MainFrame.Navigate(new GuideTourRequestView(MainFrame,FilterOptions,User,ForComplex));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
