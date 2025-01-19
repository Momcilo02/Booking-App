using BookingApp.Application.UseCases;
using BookingApp.Commands;
using BookingApp.Domain.Models;
using BookingApp.DTO;
using BookingApp.DTO.TourDTOs;
using BookingApp.WPF.View.Guide;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace BookingApp.WPF.ViewModel.Guide
{
    public class AcceptTourRequestViewModel : INotifyPropertyChanged
    {
        public Frame MainFrame { get; set; }
        public TourRequestDTO TourRequestDTO { get; set; }
        public DateTime? TourStartDate { get; set; }
        public ObservableCollection<DateTime> AvailableDates { get; set; }
        public ObservableCollection<CalendarDateRange> BlackoutDates { get; set; }

        private TourRequestService _tourRequestService;

        public User User { get; set; }
        public bool ForComplex { get; set; }
        public ICommand ConfirmCommand { get; private set; }

        public AcceptTourRequestViewModel(TourRequestDTO tourRequestDTO, Frame mainFrame, User user, bool forComplex)
        {
            ForComplex = forComplex;
            _tourRequestService = new TourRequestService();
            TourRequestDTO = tourRequestDTO;
            ConfirmCommand = new RelayCommand(ConfirmDetails, CanConfirmDetails);
            TourStartDate = null;
            MainFrame = mainFrame;
            User = user;

            // Fetch available dates
            var availableDatesList = _tourRequestService.GetAvailableDatesForPart(User.Id, TourRequestDTO.ComplexTourId, TourRequestDTO.StartDate, TourRequestDTO.EndDate);
            AvailableDates = new ObservableCollection<DateTime>(availableDatesList.Select(d => d.ToDateTime(TimeOnly.MinValue)));

            // Initialize blackout dates
            BlackoutDates = new ObservableCollection<CalendarDateRange>();
            SetBlackoutDates();
        }

        private void SetBlackoutDates()
        {
            DateTime startDate = TourRequestDTO.StartDate.ToDateTime(TimeOnly.MinValue);
            DateTime endDate = TourRequestDTO.EndDate.ToDateTime(TimeOnly.MinValue);

            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                if (!AvailableDates.Contains(date))
                {
                    BlackoutDates.Add(new CalendarDateRange(date));
                }
            }
        }

        private bool CanConfirmDetails(object obj)
        {
            if (TourStartDate == null) { return false; }

            DateTime selectedDate = TourStartDate.Value;
            return AvailableDates.Contains(selectedDate);
        }

        private void ConfirmDetails(object obj)
        {
            var tourRequests = _tourRequestService.GetAll();
            var tourRequest = tourRequests.FirstOrDefault(x => x.Id == TourRequestDTO.Id);
            if (tourRequest == null) return;

            if (TourStartDate == null) { return; }

            var isGuideAvaialable = _tourRequestService.IsGuideAvailable(User.Id, DateOnly.FromDateTime(TourStartDate.Value));
            if (!isGuideAvaialable) return;

            tourRequest.FinalDate = DateOnly.FromDateTime(TourStartDate.Value);
            tourRequest.Status = 1;
            tourRequest.GuideId = User.Id;

            _tourRequestService.Update(tourRequest);
            MainFrame.Navigate(new GuideTourRequestView(MainFrame, null, User, ForComplex));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
