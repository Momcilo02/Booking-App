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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace BookingApp.WPF.ViewModel.Tourist
{
    public class TouristHomeViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        }
        public TourDTO SelectedTour { get; set; }
        public User User { get; set; }
        public ObservableCollection<LocationDTO> Locations { get; set; }
        public ObservableCollection<LanguageDTO> Languages { get; set; }
        public TourService TourService;
        public ICommand ReserveClick { get; }
        public ICommand PdfClick { get; }
        private LocationService LocationService { get; set; }
        private LanguageService LanguageService { get; set; }
        private Frame frame;
        private Frame frame2;
        public TouristHomeViewModel(User user, Frame fr, Frame fr2)
        {
            frame = fr;
            frame2 = fr2;
            User = user;
            SelectedTour = new TourDTO();
            TourService = new TourService();
            LocationService = new LocationService();
            LanguageService = new LanguageService();
            Locations = LocationService.InitializeLocations();
            Languages = LanguageService.InitializeLanguages();
            ReserveClick = new RelayCommand(param => Reserve(param));
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
            foreach (Tour tour in TourService.GetAll())
            {
                tour.Location = LocationService.Get(tour.Location.Id);
                tour.Language = LanguageService.Get(tour.Language.Id);
                Tours.Add(new TourDTO(tour));
            }
        }
        private void Reserve(object parameter)
        {
            if (SelectedTour == null)
            {
                return;
            }
            if (SelectedTour.MaxGuests == 0)
            {


                return;
            }
            frame2.Navigate(new TourReservationView(SelectedTour, User, frame, frame2));
        }
    }
}
