using BookingApp.Application.UseCases;
using BookingApp.Commands;
using BookingApp.Domain.Models;
using BookingApp.DTO;
using BookingApp.View.Tourist;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using System.Windows.Media;
using static BookingApp.WPF.ViewModel.Tourist.PeopleReportViewModel;
using BookingApp.WPF.View.Tourist;

namespace BookingApp.WPF.ViewModel.Tourist
{
    public class CreateComplexTourRequestViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public List<TourRequestDTO> TourRequests { get; set; }
        public TourRequestService tourRequestService;
        public TourRequestDTO TourRequest { get; set; }
        public User User { get; set; }
        private ComboBox ComboBoxLocation;
        private ComboBox ComboBoxLanguage;
        private TextBox TextBoxDescription;
        private TextBox TextBoxGuest;
        private ComplexTourRequestViewModel _tourRequestViewModel;
        public int complexTourId;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ICommand CreateRequestClick { get; }
        public ICommand MenuClick { get; }
        public ICommand NextClick { get; }
        public ICommand PreviousClick { get; }
        public ICommand TutorialClick { get; }
        public int sumGuest;
        public List<string> Languages { get; set; }
        public List<string> Locations { get; set; }
        private LocationService LocationService { get; set; }
        private LanguageService LanguageService { get; set; }
        private readonly CallerWindow _callerWindow;
        public event EventHandler RequestCreated;
        public Frame frame;
        public Button NextButton;
        public Button BackButton;
        public Button CreateRequestButton;
        private TextBlock ErrorLanguage;
        private TextBlock ErrorLocation;
        private TextBlock ErrorTourNumber;
        public int NumberOfGuests { get; set; }
        private int _currentTourIndex;
        public int CurrentTourIndex
        {
            get { return _currentTourIndex; }
            set
            {
                if (value != _currentTourIndex)
                {
                    _currentTourIndex = value;
                    OnPropertyChanged(nameof(CurrentTourIndex));
                }
            }
        }

        public CreateComplexTourRequestViewModel(Button createRequest, Button backButton, Button nextButton,User user, Frame fr, ComboBox comboBoxLocation, ComboBox comboBoxLanguage, TextBox textBoxGuests, TextBox textBoxDescription, ComplexTourRequestViewModel tourRequestViewModel, CallerWindow callerWindow, TextBlock errorLanguage, TextBlock errorLocation, TextBlock errorTourNumber)
        {
            CreateRequestButton = createRequest;
            _callerWindow = callerWindow;
            User = user;
            frame = fr;
            ErrorLocation = errorLocation;
            ErrorLanguage = errorLanguage;
            ErrorTourNumber = errorTourNumber;
            NextButton = nextButton;
            BackButton = backButton;
            ComboBoxLocation = comboBoxLocation;
            ComboBoxLanguage = comboBoxLanguage;
            TextBoxGuest = textBoxGuests;
            TextBoxDescription = textBoxDescription;
            CurrentTourIndex = 1;
            Locations = new List<string>();
            Languages = new List<string>();
            StartDate = DateTime.Now;
            EndDate = DateTime.Now.AddDays(1);
            LocationService = new LocationService();
            LanguageService = new LanguageService();
            tourRequestService = new TourRequestService();
            TourRequest = new TourRequestDTO();
            TourRequests = new List<TourRequestDTO>();
            MenuClick = new RelayCommand(param => Menu(param));
            NextClick = new RelayCommand(param => Next(param));
            PreviousClick = new RelayCommand(param => Previous(param));
            CreateRequestClick = new RelayCommand(param => CreateRequest(param));
            _tourRequestViewModel = tourRequestViewModel;
            BackButton.IsEnabled = false;
            CreateRequestButton.IsEnabled = false;
            CreateRequestButton.Background = Brushes.LightGray;
            TourRequest tourRequest = tourRequestService.GetLast();
            complexTourId = tourRequest.ComplexTourId + 1;
            GetLanguages();
            GetLocations();
        }
       
        private void Menu(object parameter)
        {
            frame.Content = null;
        }

        private void Next(object parameter)
        {
            MoveToNextTour();
        }

        private void MoveToNextTour()
        {
            bool isValid = true;

            if (ComboBoxLocation.SelectedIndex == -1)
            {
                ErrorLocation.Text = "You must select a location.";
                isValid = false;
            }
            else
            {
                ErrorLocation.Text = string.Empty;
            }
            if (ComboBoxLanguage.SelectedIndex == -1)
            {
                ErrorLanguage.Text = "You must select a language.";
                isValid = false;
            }
            else
            {
                ErrorLanguage.Text = string.Empty;
            }
            if (!int.TryParse(TextBoxGuest.Text, out int numberOfGuests) || numberOfGuests <= 0)
            {
                ErrorTourNumber.Text = "Please enter a valid number of guests.";
                isValid = false;
            }
            else
            {
                ErrorTourNumber.Text = string.Empty;
            }

            if (!isValid)
            {
                return;
            }
            CurrentTourIndex++;
            ErrorTourNumber.Text = string.Empty;
            ErrorLanguage.Text = string.Empty;
            ErrorLocation.Text = string.Empty;
            BackButton.IsEnabled = true;
            CreateRequestButton.IsEnabled = true;
            CreateRequestButton.Background = Brushes.White;
            TourRequest.Location = LocationService.Get(ComboBoxLocation.SelectedIndex + 1);
            TourRequest.Language = LanguageService.Get(ComboBoxLanguage.SelectedIndex + 1);
            TourRequest.StartDate = new DateOnly(StartDate.Year, StartDate.Month, StartDate.Day);
            TourRequest.EndDate = new DateOnly(EndDate.Year, EndDate.Month, EndDate.Day);
            TourRequest.Description = TextBoxDescription.Text;
            TourRequest.TouristId = User.Id;           
            TourRequest.ComplexTourId = complexTourId;
            TourRequest request = TourRequest.ToComplexTourRequest();           
            sumGuest += numberOfGuests;
            tourRequestService.Save(request);
            TourRequests.Add(TourRequest);
            UpdateInputFields();
        }

        private void Previous(object parameter)
        {
            UpdateInputFields();
            MoveToPreviousTour();
        }

        private void MoveToPreviousTour()
        {
            ErrorTourNumber.Text = string.Empty;
            ErrorLanguage.Text = string.Empty;
            ErrorLocation.Text = string.Empty;
            CurrentTourIndex--;
            List<TourRequest> TourRequests = tourRequestService.GetComplexTourPart(complexTourId);
            TourRequest savedTourRequest = TourRequests[CurrentTourIndex - 1];
            if ((CurrentTourIndex - 1) == 0)
            {
                BackButton.IsEnabled = false;
            }
            UpdateInputFields();
        }

        private void CreateRequest(object parameter)
        {
            bool isValid = true;

            if (ComboBoxLocation.SelectedIndex == -1)
            {
                ErrorLocation.Text = "You must select a location.";
                isValid = false;
            }
            else
            {
                ErrorLocation.Text = string.Empty;
            }
            if (ComboBoxLanguage.SelectedIndex == -1)
            {
                ErrorLanguage.Text = "You must select a language.";
                isValid = false;
            }
            else
            {
                ErrorLanguage.Text = string.Empty;
            }
            if (!int.TryParse(TextBoxGuest.Text, out int numberOfGuests) || numberOfGuests <= 0)
            {
                ErrorTourNumber.Text = "Please enter a valid number of guests.";
                isValid = false;
            }
            else
            {
                ErrorTourNumber.Text = string.Empty;
            }

            if (!isValid)
            {
                return;
            }
            TourRequest.Location = LocationService.Get(ComboBoxLocation.SelectedIndex + 1);
            TourRequest.Language = LanguageService.Get(ComboBoxLanguage.SelectedIndex + 1);
            TourRequest.StartDate = new DateOnly(StartDate.Year, StartDate.Month, StartDate.Day);
            TourRequest.EndDate = new DateOnly(EndDate.Year, EndDate.Month, EndDate.Day);
            TourRequest.TouristId = User.Id;
            TourRequest.ComplexTourId = complexTourId;
            TourRequest.Description = TextBoxDescription.Text;
            TourRequest request = TourRequest.ToComplexTourRequest();
            tourRequestService.Save(request);
            TourRequests.Add(TourRequest);
            sumGuest += numberOfGuests;
            frame.Content = null;
            RequestCreated?.Invoke(this, EventArgs.Empty);
            _tourRequestViewModel.UpdateRequests();
            _tourRequestViewModel.UpdateTourRequestParts();
            _tourRequestViewModel.UpdateComplexStatus();
            List<string> guestNames = Enumerable.Range(1, sumGuest).Select(i => $"Guest {i}").ToList();
            frame.Navigate(new PeopleReportWindow(guestNames, null, null,frame));
        }

        private void GetLanguages()
        {
            Languages.Clear();
            Languages.AddRange(LanguageService.GetLanguageNames());
            ComboBoxLanguage.ItemsSource = Languages;
        }

        private void GetLocations()
        {
            Locations.Clear();
            Locations.AddRange(LocationService.GetCitiesAndCountries());
            ComboBoxLocation.ItemsSource = Locations;
        }

        private void UpdateInputFields()
        {
            if (CurrentTourIndex <= TourRequests.Count)
            {
                TourRequest tourRequest = tourRequestService.GetLast();
                List<TourRequest> TourRequests = tourRequestService.GetComplexTourPart(tourRequest.ComplexTourId);
                TourRequest savedTourRequest = TourRequests[CurrentTourIndex - 1];
                savedTourRequest.Location = LocationService.Get(savedTourRequest.Location.Id);
                savedTourRequest.Language = LanguageService.Get(savedTourRequest.Language.Id);
                string locationString = $"{savedTourRequest.Location.City}, {savedTourRequest.Location.Country}";
                ComboBoxLocation.SelectedIndex = Locations.FindIndex(location => location == locationString);
                ComboBoxLanguage.SelectedIndex = Languages.FindIndex(language => language == savedTourRequest.Language.Name);
                TextBoxDescription.Text = savedTourRequest.Description;
                TextBoxGuest.Text = savedTourRequest.GuestNumber.ToString();
                StartDate = new DateTime(savedTourRequest.StartDate.Year, savedTourRequest.StartDate.Month, savedTourRequest.StartDate.Day);
                EndDate = new DateTime(savedTourRequest.EndDate.Year, savedTourRequest.EndDate.Month, savedTourRequest.EndDate.Day);

            }
            else
            {
                ComboBoxLocation.SelectedIndex = -1;
                ComboBoxLanguage.SelectedIndex = -1;
                TextBoxDescription.Text = "";
                TextBoxGuest.Text = "";
            }
        }



    }
}

