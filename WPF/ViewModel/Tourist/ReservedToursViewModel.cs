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
    public class ReservedToursViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public TourTimeService TourTimeService { get; set; }
        public User User { get; set; }
        public ObservableCollection<LocationDTO> Locations { get; set; }
        public ObservableCollection<LanguageDTO> Languages { get; set; }
        public TourService TourService { get; set; }
        public TourReservationService TourReservationService { get; set; }
        public TourGuestService TourGuestService { get; set; }
        public ICommand MenuClick { get; }
        public ICommand ActiveTourClick { get; }
        private LocationService LocationService { get; set; }
        private LanguageService LanguageService { get; set; }
        private Frame frame;
        private Frame frame2;
        public ReservedToursViewModel(User user, Frame fr, Frame fr2)
        {
            frame = fr;
            frame2 = fr2;
            User = user;
            TourService = new TourService();
            TourTimeService = new TourTimeService();
            LocationService = new LocationService();
            LanguageService = new LanguageService();
            TourGuestService = new TourGuestService();
            TourReservationService = new TourReservationService();
            Locations = LocationService.InitializeLocations();
            Languages = LanguageService.InitializeLanguages();
            MenuClick = new RelayCommand(param => Menu(param));
            ActiveTourClick = new RelayCommand(param => ActiveTour(param));
            Tours = new ObservableCollection<TourDTO>();
            UpdateTours();
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

        public void UpdateTours()
        {
            Tours.Clear();
            foreach (Tour tour in TourService.GetReservedTours())
            {
                tour.Location = LocationService.Get(tour.Location.Id);
                tour.Language = LanguageService.Get(tour.Language.Id);
                var tourDTO = new TourDTO(tour);
                TourReservation tourReservation = TourReservationService.Get(tour.Id);
                TourTime tourTime = TourTimeService.Get(tourReservation.TourTimeId);
                tourDTO.Date = tourTime.Time;
                foreach (TourGuest tourGuest in TourGuestService.GetByTour(tour.Id))
                {
                    tourDTO.TourGuests.Add(new TourGuestDTO(tourGuest));
                }
                Tours.Add(tourDTO);
            }
        }
        private void Menu(object parameter)
        {
            frame.Navigate(new TouristHomeView(User, frame, frame2));
        }
        private void ActiveTour(object parameter)
        {
            frame.Navigate(new ActiveTourView(User, frame, frame2));
        }
    }
}
