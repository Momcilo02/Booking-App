using BookingApp.Domain.Models;
using BookingApp.DTO;
using BookingApp.Application.UseCases;
using BookingApp.WPF.ViewModel.Tourist;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System;
using BookingApp.WPF.View.Tourist;
using System.ComponentModel;

namespace BookingApp.View.Tourist
{
    public partial class TouristNotificationView : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public ObservableCollection<TouristNotificationDTO> TouristNotification { get; set; }
        public ObservableCollection<TourReservationDTO> FinishedTours { get; set; }
        public ObservableCollection<Tour> ToursByLocation { get; set; }
        public ObservableCollection<Tour> ToursByLanguage { get; set; }
        public ObservableCollection<TourGuestDTO> PresenceGuests { get; set; }
        public TourReservationService TourReservationService { get; set; }
        public TourGuestService TourGuestService { get; set; }
        public TourService TourService { get; set; }
        public LanguageService LanguageService { get; set; }
        public LocationService LocationService { get; set; }
        public TourRequestService TourRequestService { get; set; }
        public User User { get; set; }
        public Frame frame;
        public Frame fr2;
        public TouristNotificationView(User user, Frame fr, Frame frame2)
        {
            InitializeComponent();
            frame = fr;
            fr2 = frame2;
            DataContext = this;
            TourReservationService = new TourReservationService();
            TourGuestService = new TourGuestService();
            TourRequestService = new TourRequestService();
            LocationService = new LocationService();
            LanguageService = new LanguageService();
            TourService = new TourService();
            ToursByLanguage = new ObservableCollection<Tour>();
            ToursByLocation = new ObservableCollection<Tour>();
            FinishedTours = new ObservableCollection<TourReservationDTO>();
            TouristNotification = new ObservableCollection<TouristNotificationDTO>();
            PresenceGuests = new ObservableCollection<TourGuestDTO>();
            User = user;
            LoadFinishedTours();
            LoadGuests();
            LoadAcceptedTourRequests();
            LoadToursByLocation();
            LoadToursByLanguage();
            SetButtonNames();
        }
        private void SetButtonNames()
        {
            foreach (var touristNotification in TouristNotification)
            {

                var titleParts = touristNotification.Title.Split(new[] { ":" }, StringSplitOptions.None);
                if (titleParts[0] == "Finished tour")
                {
                    touristNotification.ButtonName = "   Review   ";
                }
                else
                {
                    touristNotification.ButtonName = "   Check   ";
                }


            }
        }
        private void RemoveNotification(TouristNotificationDTO notification)
        {
            TouristNotification.Remove(notification);
            OnPropertyChanged(nameof(TouristNotification));
        }

        private void NotificationClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                TouristNotificationDTO clickedNotification = button.Tag as TouristNotificationDTO;
                if (clickedNotification != null)
                {
                    var titleParts = clickedNotification.Title.Split(new[] { ":" }, StringSplitOptions.None);
                    if (titleParts[0] == "Tour by location" || titleParts[0] == "Tour by language")
                    {
                        frame.Navigate(new SelectedTourView(User, frame, fr2, clickedNotification));
                    }
                    else if (titleParts[0] == "Accepted Tour Request")
                    {
                        frame.Navigate(new SelectedRequestView(User, frame, clickedNotification));
                    }
                    else if (titleParts[0] == "Finished tour")
                    {
                        TourReservationDTO tourReservationDTO = new TourReservationDTO();
                        fr2.Navigate(new TouristReviewView(tourReservationDTO, User, fr2));
                    }
                    else
                    {
                        frame.Navigate(new ActiveTourView(User, frame, fr2));
                    }
                    RemoveNotification(clickedNotification);
                }
            }
        }

        private void MenuClick(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new TouristHomeView(User, frame, fr2));
        }
        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        }
        private void LoadGuests()
        {
            List<string> tourGuests = new List<string>();
            PresenceGuests = GetPresenceGuests();
            string Title = "Active tour presence: ";
            foreach (var guest in PresenceGuests)
            {
                string textBlock;
                textBlock = $"{guest.Name} {guest.Surname}";
                tourGuests.Add(textBlock);
            }
            TouristNotification touristNotification = new TouristNotification(Title, tourGuests);
            TouristNotification.Add(new TouristNotificationDTO(touristNotification));
        }
        private ObservableCollection<TourGuestDTO> GetPresenceGuests()
        {
            var tourGuests = new ObservableCollection<TourGuestDTO>();
            foreach (TourGuest tourGuest in TourGuestService.GetAll())
            {
                if (tourGuest.Presence == true)
                {
                    tourGuests.Add(new TourGuestDTO(tourGuest));
                }
            }
            return tourGuests;
        }

        private void LoadAcceptedTourRequests()
        {
            var acceptedTours = TourRequestService.GetAcceptedTours();

            if (acceptedTours.Any())
            {
                foreach (var acceptedTour in acceptedTours)
                {
                    List<string> acceptedTourDescription = new List<string>();
                    string description = $"Tour guide has accepted the tour request:{acceptedTour.Description}";
                    acceptedTourDescription.Add(description);
                    string date = $"Date: {acceptedTour.FinalDate}.";
                    acceptedTourDescription.Add(date);
                    TouristNotification touristNotification = new TouristNotification("Accepted Tour Request:", acceptedTourDescription);
                    TouristNotification.Add(new TouristNotificationDTO(touristNotification));
                }
            }
        }


        private void LoadFinishedTours()
        {
            FinishedTours = TourReservationService.GetFinishedTours();
            if (FinishedTours.Count > 0)
            {
                ReviewTours();
            }
        }

        private void LoadToursByLocation()
        {

            ObservableCollection<Tour> ToursByLocation = TourService.GetToursByLocation();
            foreach (Tour tour in ToursByLocation)
            {
                string title = $"Tour by location: {LocationService.GetLocationName(tour.Location.Id)}";
                List<string> Description = new List<string>();
                string TourName = $"Name:{tour.Name}";
                Description.Add(TourName);
                string TourDescription = $"Description: {tour.Description}";
                Description.Add(TourDescription);
                TouristNotification touristNotification = new TouristNotification(title, Description);
                TouristNotification.Add(new TouristNotificationDTO(touristNotification));
            }
        }
        private void LoadToursByLanguage()
        {
            ObservableCollection<Tour> ToursByLanguage = TourService.GetToursByLanguage();
            foreach (Tour tour in ToursByLanguage)
            {
                string title = $"Tour by language:{LanguageService.GetLanguageName(tour.Language.Id)}";
                List<string> Description = new List<string>();
                string TourName = $"Name:{tour.Name}";
                Description.Add(TourName);
                string TourDescription = $"Description: {tour.Description}";
                Description.Add(TourDescription);
                TouristNotification touristNotification = new TouristNotification(title, Description);
                TouristNotification.Add(new TouristNotificationDTO(touristNotification));
            }
        }

        private void ReviewTours()
        {
            foreach (var tourReservationDTO in FinishedTours)
            {
                List<string> Description = new List<string>();
                Tour tour = TourService.Get(tourReservationDTO.TourId);
                string finishedTour = $"Tour {tour.Name} is finished. You can do a review.";
                Description.Add(finishedTour);
                TouristNotification touristNotification = new TouristNotification("Finished tour:", Description);
                TouristNotification.Add(new TouristNotificationDTO(touristNotification));
            }
        }


    }
}
