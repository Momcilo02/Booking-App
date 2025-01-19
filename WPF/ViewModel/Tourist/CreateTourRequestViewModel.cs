using BookingApp.Application.UseCases;
using BookingApp.Commands;
using BookingApp.Domain.Models;
using BookingApp.DTO;
using BookingApp.View.Tourist;
using BookingApp.WPF.View.Tourist;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using static BookingApp.WPF.ViewModel.Tourist.PeopleReportViewModel;

namespace BookingApp.WPF.ViewModel.Tourist
{
    public class CreateTourRequestViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public TourRequestDTO TourRequest { get; set; }
        public TourRequestService tourRequestService;
        public User User { get; set; }
        private ComboBox ComboBoxLocation;
        private ComboBox ComboBoxLanguage;
        private TextBox TextBoxDescription;
        private TextBox TextBoxGuest;
        private TourRequestViewModel _tourRequestViewModel;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ICommand CreateRequestClick { get; }
        public ICommand MenuClick { get; }
        public ICommand TutorialClick { get; }
        public List<string> Languages { get; set; }
        public List<string> Locations { get; set; }
        private LocationService LocationService { get; set; }
        private LanguageService LanguageService { get; set; }
        private readonly CallerWindow _callerWindow;
        public event EventHandler RequestCreated;
        public Frame frame;
        public Button CreateRequestButton;
        public int NumberOfGuests { get; set; }
        private TextBlock ErrorLanguage;
        private TextBlock ErrorLocation;
        private TextBlock ErrorTourNumber;
        private string finishedText;
        public string FinishedText
        {
            get { return finishedText; }
            set
            {
                if (finishedText != value)
                {
                    finishedText = value;
                    OnPropertyChanged(nameof(FinishedText));
                }
            }
        }
        public CreateTourRequestViewModel(Button createRequest, User user, Frame fr, ComboBox comboBoxLocation, ComboBox comboBoxLanguage, TextBox textBoxGuests, TourRequestViewModel tourRequestViewModel, CallerWindow callerWindow, TextBlock errorLanguage, TextBlock errorLocation, TextBlock errorTourNumber)
        {
            CreateRequestButton = createRequest;
            _callerWindow = callerWindow;
            User = user;
            frame = fr;
            _tourRequestViewModel = tourRequestViewModel;
            ErrorLocation = errorLocation;
            ErrorLanguage = errorLanguage;
            ErrorTourNumber = errorTourNumber;
            ComboBoxLocation = comboBoxLocation;
            ComboBoxLanguage = comboBoxLanguage;
            TextBoxGuest = textBoxGuests;
            Locations = new List<string>();
            Languages = new List<string>();
            StartDate = DateTime.Now;
            EndDate = DateTime.Now.AddDays(1);
            TextBoxGuest.TextChanged += TextBoxGuests_TextChanged;
            LocationService = new LocationService();
            LanguageService = new LanguageService();
            tourRequestService = new TourRequestService();
            TourRequest = new TourRequestDTO();
            MenuClick = new RelayCommand(param => Menu(param));
            CreateRequestClick = new RelayCommand(param => CreateRequest(param));
            GetLanguages();
            GetLocations();
        }
        
        private void Menu(object parameter)
        {
            frame.Content = null;
        }
        private void TextBoxGuests_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (int.TryParse(TextBoxGuest.Text, out int numberOfGuests))
            {
                NumberOfGuests = numberOfGuests;
            }
            else
            {
                NumberOfGuests = 0;
            }
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
            TourRequest request = TourRequest.ToTourRequest();
            tourRequestService.Save(request);
            frame.Content = null;
            RequestCreated?.Invoke(this, EventArgs.Empty);
            _tourRequestViewModel.UpdateRequests();
            List<string> guestNames = Enumerable.Range(1, NumberOfGuests).Select(i => $"Guest {i}").ToList();
            frame.Navigate(new PeopleReportWindow(guestNames, null, null, frame));
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

    }
}
