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
using System.Windows.Controls;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModel.Tourist
{
    public class SelectedRequestViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public User User { get; set; }
        public Frame frame;
        public Frame frame2;
        public TouristMainView MainView { get; set; }
        public ICommand CreateRequestClick { get; }
        public ICommand StatisticsClick { get; }
        public ICommand MenuClick { get; }
        public ObservableCollection<LocationDTO> Locations { get; set; }
        public ObservableCollection<LanguageDTO> Languages { get; set; }
        public TourRequestService TourRequestService;
        public LocationService LocationService;
        public LanguageService LanguageService;
        public TouristNotificationDTO TouristNotification;
        public SelectedRequestViewModel(User user, Frame fr, TouristNotificationDTO touristNotification)
        {
            User = user;
            frame = fr;
            TouristNotification = touristNotification;
            MainView = new TouristMainView(User);
            CreateRequestClick = new RelayCommand(param => CreateRequest(param));
            MenuClick = new RelayCommand(param => Menu(param));
            StatisticsClick = new RelayCommand(param => Statistics(param));
            TourRequestService = new TourRequestService();
            LocationService = new LocationService();
            LanguageService = new LanguageService();
            Locations = new ObservableCollection<LocationDTO>();
            Languages = new ObservableCollection<LanguageDTO>();
            TourRequests = new ObservableCollection<TourRequestDTO>();
            UpdateRequests();
        }
        public void UpdateRequests()
        {
            TourRequests.Clear();
            TourRequestService.ChangeStatus();
            var titleParts = TouristNotification.Description[0].Split(new[] { ":" }, StringSplitOptions.None);
            foreach (TourRequest tourRequest in TourRequestService.GetByDescription(titleParts[1]))
            {
                tourRequest.Location = LocationService.Get(tourRequest.Location.Id);
                tourRequest.Language = LanguageService.Get(tourRequest.Language.Id);
                TourRequests.Add(new TourRequestDTO(tourRequest));
            }
        }
        private void CreateRequest(object parameter)
        {
            TourRequestViewModel tourRequestViewModel = new TourRequestViewModel(User, frame, frame2);
            frame2.Navigate(new CreateTourRequestView(User, tourRequestViewModel, frame2));
        }
        private void Menu(object parameter)
        {
            frame.Navigate(new TouristHomeView(User, frame, frame2));

        }
        private void Statistics(object parameter)
        {
            frame.Navigate(new TourRequestStatistics(User, frame, frame2));

        }
        private ObservableCollection<TourRequestDTO> _tourrequests;
        public ObservableCollection<TourRequestDTO> TourRequests
        {
            get { return _tourrequests; }
            set
            {
                _tourrequests = value;
                OnPropertyChanged(nameof(TourRequests));
            }
        }
        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        }
    }
}
