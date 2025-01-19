using BookingApp.Application.UseCases;
using BookingApp.Commands;
using BookingApp.Domain.Models;
using BookingApp.DTO;
using BookingApp.Repository;
using BookingApp.View.Tourist;
using BookingApp.WPF.View.Tourist;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static BookingApp.WPF.ViewModel.Tourist.PeopleReportViewModel;

namespace BookingApp.WPF.ViewModel.Tourist
{
    public class TourReservationViewModel : INotifyPropertyChanged
    {
        private TourTimeDTO _selectedTime;
        public TourTimeDTO SelectedTime
        {
            get { return _selectedTime; }
            set
            {
                if (_selectedTime != value)
                {
                    _selectedTime = value;
                    OnPropertyChanged(nameof(SelectedTime));
                }
            }
        }
        private readonly TextBlock ErrorDate;
        private readonly TextBlock ErrorGuests;
        public UserRepository UserRepository { get; set; }
        public ObservableCollection<TourTimeDTO> TourTimes { get; set; }
        public TourDTO SelectedTour { get; set; }
        public List<string> VoucherNames { get; set; }
        private readonly VoucherService voucherService;
        private readonly TourTimeService tourTimeService;
        private readonly TourReservationService tourReservationService;
        public User User { get; set; }
        private readonly TextBox TextBoxGuests;
        private readonly ComboBox ComboBoxVoucher;
        public ICommand CancelClick { get; }
        public ICommand MenuClick { get; }
        public ICommand TutorialClick { get; }
        public ICommand ReserveClick { get; }
        private readonly CallerWindow _callerWindow;
        public Frame frame { get; set; }
        public Frame frame2 { get; set; }

        public TourReservationViewModel(TourDTO selectedTour, User user, Frame fr, Frame fr2, TextBox textBoxGuests, ComboBox comboBoxVoucher, CallerWindow callerWindow, TextBlock errorDate, TextBlock errorGuests)
        {

            frame = fr;
            frame2 = fr2;
            _callerWindow = callerWindow;
            voucherService = new VoucherService();
            UserRepository = new UserRepository();
            tourTimeService = new TourTimeService();
            tourReservationService = new TourReservationService();
            SelectedTour = selectedTour;
            User = user;
            SelectedTime = new TourTimeDTO();
            TourTimes = new ObservableCollection<TourTimeDTO>();
            TextBoxGuests = textBoxGuests;
            ComboBoxVoucher = comboBoxVoucher;
            ErrorDate = errorDate;
            ErrorGuests = errorGuests;
            TextBoxGuests.Text = SelectedTour.MaxGuests.ToString();
            VoucherNames = new List<string>();
            ReserveClick = new RelayCommand(param => Reserve(param));
            MenuClick = new RelayCommand(param => Menu(param));
            GetVoucherNames();
            GetTourDates();
            CheckVoucherAvailability();
            ComboBoxVoucher.SelectionChanged += ComboBox_Voucher;
        }
        
        private void Reserve(object parameter)
        {
            bool isValid = true;

            if (SelectedTime == null || SelectedTime.Id == 0)
            {
                ErrorDate.Text = "You must select a date.";
                isValid = false;
            }
            else
            {
                ErrorDate.Text = string.Empty;
            }
            if (!int.TryParse(TextBoxGuests.Text, out int numberOfGuests) || numberOfGuests <= 0)
            {
                ErrorGuests.Text = "Please enter a valid number of guests.";
                isValid = false;
            }
            else
            {
                ErrorGuests.Text = string.Empty;
            }

            if (numberOfGuests > SelectedTour.MaxGuests)
            {
                ErrorGuests.Text = "Please enter a valid number of guests.";
                isValid = false;
            }
            else
            {
                ErrorGuests.Text = string.Empty;
            }
            if (!isValid)
            {
                return;
            }
            List<string> guestNames = Enumerable.Range(1, numberOfGuests).Select(i => $"Guest {i}").ToList();
            DeleteReservedVoucher();
            User.Counter++;
            UserRepository.Update(User);
            var reservation = new TourReservation(User.Id, SelectedTour.Id, 1, 0, -1);
            tourReservationService.Save(reservation);
            frame2.Content = null;
            frame2.Navigate(new PeopleReportWindow(guestNames, SelectedTour, User, frame2));
            if (User.Counter == 5)
            {
                DateOnly expiryDate = DateOnly.FromDateTime(DateTime.Today.AddMonths(6));
                Voucher voucher = new Voucher(expiryDate, "OPQ532", User.Id, 0, "Reserved 5 tours");
                voucherService.Save(voucher);
                MessageBox.Show("Congrats! You have received your voucher.");

                VoucherView voucherView = new VoucherView(User, frame, frame2);
                frame.Navigate(new VoucherView(User, frame, frame2));
            }
        }

        private void GetTourDates()
        {
            TourTimes.Clear();
            var tourTimesForTour = tourTimeService.GetTourTimesForTour(SelectedTour.Id);
            tourTimesForTour.ForEach(tourTime => TourTimes.Add(new TourTimeDTO(tourTime)));
        }
        private void GetVoucherNames()
        {
            VoucherNames.Clear();
            VoucherNames.AddRange(voucherService.GetVoucherNamesForUser(User.Id));
            ComboBoxVoucher.ItemsSource = VoucherNames;
        }
        private void Menu(object parameter)
        {
            frame2.Content = null;
        }
        private void DeleteReservedVoucher()
        {
            if (ComboBoxVoucher.SelectedIndex != -1)
            {
                string selectedVoucherName = ComboBoxVoucher.SelectedItem.ToString();
                Voucher selectedVoucher = voucherService.GetByName(selectedVoucherName);
                voucherService.Delete(selectedVoucher);
            }
        }

        private void ComboBox_Voucher(object sender, SelectionChangedEventArgs e)
        {
            CheckVoucherAvailability();
        }

        private void CheckVoucherAvailability()
        {
            ComboBoxVoucher.IsEnabled = VoucherNames != null && VoucherNames.Any();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
