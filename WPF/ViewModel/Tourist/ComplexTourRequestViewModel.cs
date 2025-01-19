using BookingApp.Application.UseCases;
using BookingApp.Commands;
using BookingApp.Domain.Models;
using BookingApp.DTO;
using BookingApp.View.Tourist;
using BookingApp.WPF.View.Tourist;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModel.Tourist
{
    public class ComplexTourRequestViewModel:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public User User { get; set; }
        public Frame frame;
        public Frame frame2;
        public TouristMainView MainView { get; set; }
        public ICommand CreateRequestClick { get; }
        public ICommand StatisticsClick { get; }
        public ICommand MenuClick { get; }
        public ICommand TourRequestClick { get; }
        private ObservableCollection<TourRequestDTO> _complexTourRequests;
        public ObservableCollection<TourRequestDTO> ComplexTourRequests
        {
            get { return _complexTourRequests; }
            set
            {
                if (_complexTourRequests != value)
                {
                    _complexTourRequests = value;
                    OnPropertyChanged(nameof(ComplexTourRequests));
                }
            }
        }
        private ObservableCollection<TourRequestDTO> _complexTourRequestsParts;
        public ObservableCollection<TourRequestDTO> ComplexTourRequestsParts
        {
            get { return _complexTourRequestsParts; }
            set
            {
                if (_complexTourRequestsParts != value)
                {
                    _complexTourRequestsParts = value;
                    OnPropertyChanged(nameof(ComplexTourRequestsParts));
                }
            }
        }
        private string _statusComplex;
        public string StatusComplex
        {
            get { return _statusComplex; }
            set
            {
                if (_statusComplex != value)
                {
                    _statusComplex = value;
                    OnPropertyChanged(nameof(StatusComplex));
                }
            }
        }

        public ObservableCollection<LocationDTO> Locations { get; set; }
        public ObservableCollection<LanguageDTO> Languages { get; set; }
        public TourRequestService TourRequestService;
        public LocationService LocationService;
        public LanguageService LanguageService;
        public ComplexTourRequestViewModel(User user, Frame fr,Frame fr2)
        {
            User = user;
            frame = fr;
            frame2=fr2;
            MainView = new TouristMainView(User);
            CreateRequestClick = new RelayCommand(param => CreateRequest(param));
            MenuClick = new RelayCommand(param => Menu(param));
            StatisticsClick = new RelayCommand(param => Statistics(param));
            TourRequestClick = new RelayCommand(param => TourRequest(param));
            TourRequestService = new TourRequestService();
            LocationService = new LocationService();
            LanguageService = new LanguageService();
            Locations = new ObservableCollection<LocationDTO>();
            Languages = new ObservableCollection<LanguageDTO>();
            ComplexTourRequests = new ObservableCollection<TourRequestDTO>();
            ComplexTourRequestsParts = new ObservableCollection<TourRequestDTO>();
            UpdateRequests();
            UpdateTourRequestParts();
            UpdateComplexStatus();
        }
        public void UpdateRequests()
        {
            ComplexTourRequests.Clear(); 

            TourRequestService.ChangeStatus();

            foreach (TourRequest tourRequest in TourRequestService.GetComplexTours())
            {
                tourRequest.Location = LocationService.Get(tourRequest.Location.Id);
                tourRequest.Language = LanguageService.Get(tourRequest.Language.Id);
                ComplexTourRequests.Add(new TourRequestDTO(tourRequest));
                
            }
        }
        public void UpdateComplexStatus()
        {
            ComplexTourRequests.Clear();

            TourRequestService.ChangeStatus();
            for (int i = 1; i <= 10; i++)
            {
                List<TourRequest> tourRequests = TourRequestService.GetComplexTourPart(i);
                foreach (TourRequest tourRequest in tourRequests)

                    if (tourRequest.Status != 1)
                        {
                        tourRequests[1].Status = 0;
                        TourRequestService.Update(tourRequests[1]);
                        OnPropertyChanged(nameof(StatusComplex));
                    }                   
                }
            }
        public void UpdateTourRequestParts()
        {
            ComplexTourRequestsParts.Clear();
            foreach (var request in ComplexTourRequests.OrderBy(r => r.ComplexTourId))
            {
                ComplexTourRequestsParts.Add(request);
            }
        }
        
        private void CreateRequest(object parameter)
        {
            frame2.Navigate(new CreateComplexTourRequest(User, this, frame2));
        }
        private void TourRequest(object parameter)
        {
            frame.Navigate(new TourRequestView(User, frame,frame2));
        }
        private void Menu(object parameter)
        {
            frame.Navigate(new TouristHomeView(User, frame, frame2));

        }
        private void Statistics(object parameter)
        {
            frame.Navigate(new TourRequestStatistics(User, frame, frame2));

        }
        private ObservableCollection<TourRequestDTO> _tourrequests;
        public ObservableCollection<TourRequestDTO> TourRequests
        {
            get { return _tourrequests; }
            set
            {
                _tourrequests = value;
                OnPropertyChanged(nameof(TourRequests));
            }
        }
        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        }
    }
}
