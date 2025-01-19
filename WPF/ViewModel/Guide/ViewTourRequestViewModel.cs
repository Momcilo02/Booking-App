using BookingApp.Application.UseCases;
using BookingApp.Commands;
using BookingApp.Domain.Models;
using BookingApp.DTO;
using BookingApp.WPF.View;
using BookingApp.WPF.View.Guide;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModel.Guide
{
    public class ViewTourRequestViewModel : INotifyPropertyChanged
    {
        public TourRequestDTO TourRequest { get; private set; }

        private TourRequestService _tourRequestService { get; set; }
        public Frame MainNavigationFrame { get; set; }
        public ICommand AcceptCommand { get; private set; }
        public ICommand DeclineCommand { get; private set; }

        public User User { get; set; }
        public bool ForComplex { get; set; }

        public ViewTourRequestViewModel(Frame mainNavigationFrame, TourRequestDTO tourRequest, User user, bool forComplex)
        {
            User = user;
            TourRequest = tourRequest;
            MainNavigationFrame = mainNavigationFrame;
            _tourRequestService = new TourRequestService();

            AcceptCommand = new RelayCommand(AcceptTourRequest);
            DeclineCommand = new RelayCommand(DeclineTourRequest);
            ForComplex = forComplex;
        }

        private void AcceptTourRequest(object obj)
        {
            if(!ForComplex) MainNavigationFrame.Navigate(new AcceptTourRequestView(TourRequest, MainNavigationFrame, User, ForComplex));

            var availableDates = _tourRequestService.GetAvailableDatesForPart(User.Id, TourRequest.ComplexTourId, TourRequest.StartDate, TourRequest.EndDate);

            if (availableDates.Count == 0)
            {
                MessageBox.Show("There are no available dates for this part of the complex tour.", "No Available Dates", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            MainNavigationFrame.Navigate(new AcceptTourRequestView(TourRequest, MainNavigationFrame, User, ForComplex));
        }


        private void DeclineTourRequest(object obj)
        {
            var tourRequests = _tourRequestService.GetAll();
            var tourRequest = tourRequests.FirstOrDefault(x=>x.Id == TourRequest.Id);
            if (tourRequest == null) return;

            tourRequest.Status = -1;
            _tourRequestService.Update(tourRequest);
            MainNavigationFrame.Navigate(new GuideTourRequestView(MainNavigationFrame,null, User,ForComplex));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
