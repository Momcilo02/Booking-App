using BookingApp.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using BookingApp.Commands;
using BookingApp.Domain.Models;
using BookingApp.Application.UseCases;
using static BookingApp.WPF.ViewModel.Tourist.PeopleReportViewModel;
using System.Windows.Media;

namespace BookingApp.WPF.ViewModel.Tourist
{
    public class PeopleReportViewModel : INotifyPropertyChanged
    {
        public event EventHandler<List<string>> GuestDataUpdated;
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly TourService _tourService;
        private readonly TourReservationService _reservationService;
        private readonly TourGuestService _tourGuestService;
        public User User { get; set; }
        private List<string> guestNames;
        private int currentGuestIndex = 0;
        private TourTimeDTO SelectedTime { get; set; }
        private TextBox TextBoxFirstName;
        private TextBox TextBoxLastName;
        private TextBox TextBoxAge;
        private TextBlock ValidationFirstName;
        private TextBlock ValidationLastName;
        private TextBlock ValidationAge;
        private TextBlock FinalMessage;
        private readonly PeopleReportService _peopleReportService;
        public ICommand PreviousClick { get; }
        public System.Windows.Controls.Frame fr { get; set; }
        public ICommand NextClick { get; }
        public Button PreviousButton;
        public Button NextButton;
        private TourDTO SelectedTour { get; set; }
        public enum CallerWindow
        {
            CreateTourRequestView,
            TourReservationView
        }
        private CallerWindow _callerWindow;
        private string currentGuestLabel;
        public string CurrentGuestLabel { get => currentGuestLabel; set { if (currentGuestLabel != value) { currentGuestLabel = value; OnPropertyChanged(nameof(CurrentGuestLabel)); } } }
        public PeopleReportViewModel(List<string> guests, TourDTO selectedTour, User user, System.Windows.Controls.Frame frame, TextBox textBoxFirstName, TextBox textBoxLastName, TextBox textBoxAge, TextBlock validationFirstName, TextBlock validationLastName, TextBlock validationAge, TextBlock finalMessage, Button previous, Button next)
        {
            User = user;
            NextButton = next;
            PreviousButton = previous;
            guestNames = guests ?? new List<string>();
            SelectedTour = selectedTour;
            TextBoxFirstName = textBoxFirstName;
            TextBoxLastName = textBoxLastName;
            TextBoxAge = textBoxAge;
            fr = frame;
            ValidationFirstName = validationFirstName;
            ValidationLastName = validationLastName;
            ValidationAge = validationAge;
            FinalMessage = finalMessage;
            _reservationService = new TourReservationService();
            _tourGuestService = new TourGuestService();
            _tourService = new TourService();
            _peopleReportService = new PeopleReportService();
            PreviousClick = new RelayCommand(param => Previous(param));
            NextClick = new RelayCommand(param => Next(param));
            PreviousButton.IsEnabled = false;
            PreviousButton.Background = Brushes.LightGray;
            ResetTextBoxes();
            DisplayCurrentGuestData();
        }

        private void Next(object parameter)
        {
            if (ValidateFields())
            {
                SaveCurrentGuestData();
                MoveToNextGuest();
            }
        }

        private void Previous(object parameter)
        {
            if (ValidateFields())
            {
                SaveCurrentGuestData();
                MoveToPreviousGuest();
            }
        }
        private bool ValidateFields()
        {
            string firstName = TextBoxFirstName.Text.Trim();
            string lastName = TextBoxLastName.Text.Trim();
            string ageText = TextBoxAge.Text.Trim();
            bool isValid = true;

            if (string.IsNullOrEmpty(firstName))
            {
                ValidationFirstName.Text = "First name cannot be empty.";
                isValid = false;
            }
            else
            {
                ValidationFirstName.Text = "";
            }

            if (string.IsNullOrEmpty(lastName))
            {
                ValidationLastName.Text = "Last name cannot be empty.";
                isValid = false;
            }
            else
            {
                ValidationLastName.Text = "";
            }

            if (!int.TryParse(ageText, out int age) || age < 0)
            {
                ValidationAge.Text = "Age must be a valid non-negative number.";
                isValid = false;
            }
            else
            {
                ValidationAge.Text = "";
            }

            return isValid;
        }

        private void SaveCurrentGuestData()
        {

            if (SelectedTour != null)
            {
                _peopleReportService.SaveGuestDataForTourReservation(guestNames, currentGuestIndex, SelectedTour, SelectedTime, User, TextBoxFirstName.Text, TextBoxLastName.Text, TextBoxAge.Text);
            }
            else
            {
                _peopleReportService.SaveGuestDataForTourRequest(guestNames, currentGuestIndex, TextBoxFirstName.Text, TextBoxLastName.Text, TextBoxAge.Text);
            }
        }
        private void DisplayCurrentGuestData()
        {
            if (currentGuestIndex < guestNames.Count)
            {
                string[] parts = guestNames[currentGuestIndex].Split(',');
                if (parts.Length >= 2 && parts[1].Contains(":"))
                {
                    string[] nameParts = parts[0].Split(' ');
                    TextBoxFirstName.Text = nameParts.ElementAtOrDefault(0) ?? "";
                    TextBoxLastName.Text = nameParts.ElementAtOrDefault(1) ?? "";
                    TextBoxAge.Text = parts[1].Split(':')[1].Trim();
                    CurrentGuestLabel = $"Guest {currentGuestIndex + 1}";
                }
                else
                {
                    ResetTextBoxes();
                }
            }
            else
            {
                ResetTextBoxes();
            }
        }

        private async void MoveToNextGuest()
        {
            currentGuestIndex++;
            if (currentGuestIndex == (guestNames.Count - 1))
            {
                NextButton.Content = "Finish";
            }
            PreviousButton.IsEnabled = true;
            PreviousButton.Background = Brushes.White;
            if (currentGuestIndex >= guestNames.Count)
            {
                FinalMessage.Text = "Tour Guests saved successfully!\n Closing...";

                await Task.Delay(3000);

                GuestDataUpdated?.Invoke(this, guestNames);
                fr.Content = null;
                return;
            }

            DisplayCurrentGuestData();
        }

        private void MoveToPreviousGuest()
        {

            currentGuestIndex--;
            NextButton.Content = "Next";
            DisplayCurrentGuestData();
        }

        private void ResetTextBoxes()
        {
            CurrentGuestLabel = $"Guest {currentGuestIndex + 1}";
            TextBoxFirstName.Text = "";
            TextBoxLastName.Text = "";
            TextBoxAge.Text = "";
        }



        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

