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
    public class TourRequestInputViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<LocationDTO> Locations { get; set; }
        public ObservableCollection<Language> Languages { get; set; }

        private LanguageService _languageService { get; set; }   
        private LocationService _locationService { get; set; }   
        private TourRequestService _tourRequestService { get; set; }   

        public User User { get; set; }
        private LocationDTO _selectedLocation;
        public LocationDTO SelectedLocation
        {
            get => _selectedLocation;
            set
            {
                _selectedLocation = value;
                OnPropertyChanged(nameof(SelectedLocation));
            }
        }

        private Language _selectedLanguage;
        public Language SelectedLanguage
        {
            get => _selectedLanguage;
            set
            {
                _selectedLanguage = value;
                OnPropertyChanged(nameof(SelectedLanguage));
            }
        }

        private int? _year;
        public int? Year
        {
            get => _year;
            set
            {
                _year = value;
                OnPropertyChanged(nameof(Year));
            }
        }

        public Frame MainFrame { get; set; }

        public ICommand ConfirmCommand { get; private set; }

        public TourRequestInputViewModel(Frame mainFrame, User user)
        {
            User = user;
            MainFrame = mainFrame;
            _languageService = new LanguageService();
            _tourRequestService = new TourRequestService();
            _locationService = new LocationService();

            Locations = new ObservableCollection<LocationDTO>(_locationService.GetAll().Select(x => new LocationDTO(x)).ToList()); 
            Languages = new ObservableCollection<Language>(_languageService.GetAll());
            ConfirmCommand = new RelayCommand(ExecuteConfirm);
        }

        private void ExecuteConfirm(object parameter)
        {
            var statistics = _tourRequestService.GetTourRequestStatistics(SelectedLocation?.Id,SelectedLanguage?.Id,Year);
            MainFrame.Navigate(new TourRequestStatisticsView(MainFrame,statistics,SelectedLocation,SelectedLanguage, User));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
