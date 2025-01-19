using BookingApp.Application.UseCases;
using BookingApp.Commands;
using BookingApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModel
{
    public class ChangeAccommodationReservationViewModel : INotifyPropertyChanged
    {

        public User Guest { get; set; }
        private Window _window;
        public AccommodationReservation AccommodationReservation { get; set; }
        private AccommodationReservationRequestService _requestService;

        private DateTime startDate;
        public DateTime StartDate
        {
            get { return startDate; }
            set
            {
                if (startDate != value)
                {
                    startDate = value;
                    OnPropertyChanged(nameof(StartDate));
                }
            }

        }
        private DateTime endDate;
        public DateTime EndDate
        {
            get { return endDate; }
            set
            {
                if (endDate != value)
                {
                    endDate = value;
                    OnPropertyChanged(nameof(EndDate));
                }
            }
        }
        public ICommand CancelClick { get; }
        public ICommand ConfirmClick { get; }
        private void Cancel(object parameter)
        {
            _window.Close();
        }
        private void Confirm(object parameter)
        {
            if (!CheckDatesInput())
            {
                DateTime oldStartDate = AccommodationReservation.StartDate.ToDateTime(TimeOnly.Parse("10:00PM"));
                DateTime oldEndDate = AccommodationReservation.EndDate.ToDateTime(TimeOnly.Parse("10:00PM"));
                MessageBox.Show("Incorect length of stay, previous length was:" + (oldEndDate - oldStartDate).TotalDays.ToString());
                return;
            }
            DateOnly start = DateOnly.FromDateTime(startDate);
            DateOnly end = DateOnly.FromDateTime(endDate);
            _requestService.Save(new AccommodationReservationRequest(AccommodationReservation, start, end, Enumeration.AccommodationReservationRquest.InProcess, string.Empty));

            _window.Close();
        }

        private bool CheckDatesInput()
        {
            DateTime oldStartDate = AccommodationReservation.StartDate.ToDateTime(TimeOnly.Parse("10:00PM"));
            DateTime oldEndDate = AccommodationReservation.EndDate.ToDateTime(TimeOnly.Parse("10:00PM"));
            if ((EndDate - StartDate).Days != (oldEndDate - oldStartDate).TotalDays)
            {
                MessageBox.Show((EndDate - StartDate).Days.ToString() + "     " + (oldEndDate - oldStartDate).TotalDays.ToString());
                return false;
            }
            return true;
        }


        public ChangeAccommodationReservationViewModel()
        {

        }

        public ChangeAccommodationReservationViewModel(User user, AccommodationReservation reservation, Window window)
        {
            Guest = user;
            AccommodationReservation = reservation;
            _window = window;
            CancelClick = new RelayCommand(param => Cancel(param));
            ConfirmClick = new RelayCommand(param => Confirm(param));
            EndDate = DateTime.Now;
            StartDate = DateTime.Now;
            _requestService = new AccommodationReservationRequestService();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        }
    }
}
