using BookingApp.Application.UseCases;
using BookingApp.Commands;
using BookingApp.Domain.Models;
using BookingApp.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Xceed.Wpf.Toolkit;
using Xceed.Wpf.Toolkit.Core.Converters;

namespace BookingApp.WPF.ViewModel
{
    public class ReservationRequestViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<AccommodationReservationRequestDTO> _requests { get; set; }
        private Window _window;
        public event PropertyChangedEventHandler? PropertyChanged;
        private AccommodationReservationRequestService _requsetsService;
        private AccommodationReservationService _reservationService;
        public AccommodationReservationRequestDTO selectedRequest { get; set; }
        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public ReservationRequestViewModel(Window window)
        {
            _requests = new ObservableCollection<AccommodationReservationRequestDTO>();
            _requsetsService = new AccommodationReservationRequestService();
            _reservationService = new AccommodationReservationService();
            selectedRequest = new AccommodationReservationRequestDTO();
            _window = window;
            RejectClick = new RelayCommand(param => Cancel(param));
            ConfirmClick = new RelayCommand(param => Confirm(param));
            Update();
        }
        private void Cancel(object parameter)
        {
            selectedRequest.Status = Enumeration.AccommodationReservationRquest.Rejected;
            _requsetsService.Save(selectedRequest.ToAccommodationReservationRequest());
            Update();
        }
        private void Confirm(object parameter)
        {
            selectedRequest.Status = Enumeration.AccommodationReservationRquest.Approved;
            selectedRequest.OldReservation.StartDate = DateOnly.Parse(selectedRequest.NewStartDate);
            selectedRequest.OldReservation.EndDate = DateOnly.Parse(selectedRequest.NewEndDate);
            
            
            TimeSpan duration = DateOnly.Parse(selectedRequest.NewEndDate).ToDateTime(TimeOnly.Parse("10:00PM")) - DateOnly.Parse(selectedRequest.NewStartDate).ToDateTime(TimeOnly.Parse("10:00PM"));
            int days = duration.Days;
            foreach (var reservation in _reservationService.GetAll())
            {
                if ((IsDateFree(DateOnly.Parse(selectedRequest.NewStartDate).ToDateTime(TimeOnly.Parse("10:00PM")), days,reservation)) && (selectedRequest.OldReservation.Accommodation.Id ==reservation.Accommodation.Id))

                {
                    System.Windows.MessageBox.Show("Invalid date");

                }
            }
            _requsetsService.Save(selectedRequest.ToAccommodationReservationRequest());
            _reservationService.Update(selectedRequest.OldReservation);

            Update();
        }
        private void Update()
        {
            _requests.Clear();
            foreach (var request in _requsetsService.GetAll())
            {
                request.OldReservation = FindReservation(request.OldReservation.Id);

                if (!_requsetsService.GetAllRejectedOrApprovedIds().Contains(request.OldReservation.Id) && request.Status == Enumeration.AccommodationReservationRquest.InProcess)
                {
                    _requests.Add(new AccommodationReservationRequestDTO(request));
                }
            }
        }
        private bool IsDateFree(DateTime start, int length, AccommodationReservation reservation)
        {
            return IsOverlap(start, length, reservation);
        }

        private bool IsOverlap(DateTime start, int length, AccommodationReservation reservation)
        {
            DateTime reservationStartDate = reservation.StartDate.ToDateTime(TimeOnly.Parse("10:00PM"));
            DateTime reservationEndDate = reservation.EndDate.ToDateTime(TimeOnly.Parse("10:00PM"));


            return IsWithinRange(start, reservationStartDate, reservationEndDate) ||
                    IsWithinRange(start.AddDays(length), reservationStartDate, reservationEndDate) ||
                    start < reservationStartDate && start.AddDays(length) > reservationEndDate;
        }

        private bool IsWithinRange(DateTime dateCandidate, DateTime start, DateTime end)
        {
            return dateCandidate >= start && dateCandidate <= end;
        }

        private AccommodationReservation FindReservation(int id)
        {
            return _reservationService.Get(id);
        }
        public ICommand RejectClick { get; set; }
        public ICommand ConfirmClick { get; set; }
    }
}
