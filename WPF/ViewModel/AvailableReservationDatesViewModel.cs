using BookingApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.DTO;
using BookingApp.Application.UseCases;
using System.Data;
using Microsoft.VisualBasic.ApplicationServices;
using System.Windows.Input;
using BookingApp.Commands;
using BookingApp.WPF.View.Guest;
using System.Windows;


namespace BookingApp.WPF.ViewModel
{
    public class AvailableReservationDatesViewModel : BindableBase
    {
        public Domain.Models.User User { get; set; }
        public List<DateOnly> StartDates { get; set; }
        public List<DateOnly> EndDates { get; set; }
        public string Title { get; set; }

        private ObservableCollection<AccommodationReservationDTO> reservations;
        public ObservableCollection<AccommodationReservationDTO> Reservations
        {
            get { return reservations; }
            set
            {
                if (reservations != value)
                {
                    reservations = value;
                    OnPropertyChanged(nameof(Reservations));
                }
            }
        }
        private AccommodationReservationDTO selectedReservation;
        public AccommodationReservationDTO SelectedReservation
        {
            get { return selectedReservation; }
            set
            {
                if (selectedReservation != value)
                {
                    selectedReservation = value;
                    OnPropertyChanged(nameof(SelectedReservation));
                }
            }
        }
        public Accommodation Accommodation { get; set; }
        public string NumberOfGuests { get; set; }
        private AccommodationReservationService reservationService;
        public ICommand CancelClick { get; }
        public ICommand ConfirmClick { get; }
        public AvailableReservationDatesViewModel()
        {
            User = new Domain.Models.User();
            reservationService = new AccommodationReservationService();
            Reservations = new ObservableCollection<AccommodationReservationDTO>();
            Title = "Available Dates For:";
            Accommodation = new Accommodation();
            CancelClick = new RelayCommand(param => Cancel(param));
            ConfirmClick = new RelayCommand(param => Confirm(param));
            EndDates = new List<DateOnly>();
            StartDates = new List<DateOnly>();
            NumberOfGuests = "";
        }
        private void Confirm(object parameter)
        {
            Window win = (Window)parameter;
            if (SelectedReservation != null)
            {
                DateOnly ChosenStart = DateOnly.FromDateTime((DateTime)SelectedReservation.StartDate);
                DateOnly ChosenEnd = DateOnly.FromDateTime((DateTime)SelectedReservation.EndDate);
                AccommodationReservation reservation = new AccommodationReservation(Accommodation, User, ChosenStart, ChosenEnd, Convert.ToInt32(NumberOfGuests));
                reservationService.Save(reservation);
                MessageBox.Show("You have successfully reserved an accommodation");
                win.Close();
            }
            else
            {
                MessageBox.Show("You must select one date before you press confirm");
                return;
            }
            
        }
        private void Cancel(object window)
        {
            Window win = (Window)window;
            win.Close();
        }
        public void Initialize(Domain.Models.User user, List<DateOnly> startDates, List<DateOnly> endDates, Accommodation accommodation, string numberOfGuests)
        {
            //foreach (DateOnly dateOnly in startDates) { 
            //    MessageBox.Show(dateOnly.ToString());
            //}
            User = user;
            StartDates = startDates;
            EndDates = endDates;
            Accommodation = accommodation;
            NumberOfGuests = numberOfGuests;
            InitializeReservations();
        }
        private void InitializeReservations()
        {
            Reservations.Clear();
            for (int i = 0; i < StartDates.Count; i++)
            {
                Reservations.Add(new AccommodationReservationDTO(-1, StartDates[i], EndDates[i], User.Id, Accommodation, NumberOfGuests));
            }
        }

        public override void Demo()
        {
            throw new NotImplementedException();
        }
    }
}
