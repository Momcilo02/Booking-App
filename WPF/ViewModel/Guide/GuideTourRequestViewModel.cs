using BookingApp.Application.UseCases;
using BookingApp.Commands;
using BookingApp.Domain.Models;
using BookingApp.DTO;
using BookingApp.WPF.View.Guide;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModel.Guide
{
    public class GuideTourRequestViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<TourRequestDTO> TourRequests { get; private set; }

        private TourRequestService _tourRequestService;
        private LocationService _locationService;
        public User User { get; set; }

        public TourRequestFilterOptions FilterOptions { get; set; }
        public Frame MainNavigationFrame { get; set; }
        public ICommand ViewCommand { get; private set; }
        public ICommand StatisticsCommand { get; private set; }

        public bool ForComplex { get; set; }

        public GuideTourRequestViewModel(Frame mainNavigationFrame, TourRequestFilterOptions filterOptions,User user, bool forComplex)
        {
            User = user;
            ForComplex = forComplex;
            _tourRequestService = new TourRequestService();
            _locationService = new LocationService();
            FilterOptions = filterOptions;

            LoadTourRequests();
            ViewCommand = new RelayCommand(ViewTourRequest);
            StatisticsCommand = new RelayCommand(ShowStatistics);
            MainNavigationFrame = mainNavigationFrame;

        }

        private void ShowStatistics(object parameter)
        {
            MainNavigationFrame.Navigate(new TourRequestInputView(MainNavigationFrame, User));
        }

        private List<TourRequest>  FilterTourRequests(List<TourRequest> tourRequests) 
        {
            if (FilterOptions == null) return tourRequests;

            if(FilterOptions.SelectedLanguage != null) 
            {
                tourRequests = tourRequests.Where(x=>x.Language.Id == FilterOptions.SelectedLanguage.Id).ToList();
            }

            if(FilterOptions.SelectedCountry!="" && FilterOptions.SelectedCity!= "") 
            {

                tourRequests = tourRequests.Where(x => x.Location.City == FilterOptions.SelectedCity && x.Location.Country == FilterOptions.SelectedCountry).ToList();
            }

            if (FilterOptions.MaxNumberOfTourists.HasValue) 
            {
                tourRequests = tourRequests.Where(x => x.GuestNumber== FilterOptions.MaxNumberOfTourists.Value).ToList();

            }
            if(FilterOptions.StartDate.HasValue && FilterOptions.EndDate.HasValue) 
            {
                tourRequests = tourRequests.Where(x => x.StartDate > DateOnly.FromDateTime(FilterOptions.StartDate.Value) && x.EndDate < DateOnly.FromDateTime(FilterOptions.EndDate.Value)).ToList();

            }

            return tourRequests;
        }

        private void LoadTourRequests()
        {
            var tourRequests = _tourRequestService.GetAll().Where(x => x.Status == 0 && x.ComplexTourId == 0).ToList();
            if (ForComplex) 
            {
                tourRequests = _tourRequestService.GetComplexTours();
                tourRequests = tourRequests.Where(x => x.Status == 0).ToList();
            }
            tourRequests = FilterTourRequests(tourRequests);
            var tourRequestDtos = new List<TourRequestDTO>();
            foreach (var tourRequest in tourRequests) 
            {
                var location = _locationService.Get(tourRequest.Location.Id);
                var tourRequestDto = new TourRequestDTO(tourRequest);
                tourRequestDto.Location.City = location.City;
                tourRequestDto.Location.Country = location.Country;
                tourRequestDtos.Add(tourRequestDto);
            }
            TourRequests = new ObservableCollection<TourRequestDTO>(tourRequestDtos);
        }

        private void ViewTourRequest(object parameter)
        {
            if (parameter is TourRequestDTO tourRequest)
            {
                MainNavigationFrame.Navigate(new ViewTourRequestView(MainNavigationFrame, tourRequest, User,ForComplex));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
