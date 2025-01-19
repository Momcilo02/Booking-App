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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModel.Tourist
{
    public class TourRequestViewModel:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public User User { get; set; }
        public Frame frame;
        public Frame frame2;
        public TouristMainView MainView { get; set; }
        public ICommand CreateRequestClick { get; }
        public ICommand StatisticsClick { get; }
        public ICommand MenuClick {  get; }
        public ICommand ComplexToursClick {  get; }
        public ObservableCollection<LocationDTO> Locations { get; set; }
        public ObservableCollection<LanguageDTO> Languages { get; set; }
        public TourRequestService TourRequestService;
        public LocationService LocationService;
        public LanguageService LanguageService;
        public TourRequestViewModel(User user,Frame fr,Frame fr2) {
            User = user;
            frame = fr;
            frame2= fr2;
            MainView = new TouristMainView(User);
            CreateRequestClick = new RelayCommand(param => CreateRequest(param));
            MenuClick = new RelayCommand(param => Menu(param));
            StatisticsClick = new RelayCommand(param => Statistics(param));
            ComplexToursClick = new RelayCommand(param => ComplexTour(param));
            TourRequestService =new TourRequestService();
            LocationService=new LocationService();
            LanguageService=new LanguageService();
            Locations=new ObservableCollection<LocationDTO>();
            Languages=new ObservableCollection<LanguageDTO>();
            TourRequests=new ObservableCollection<TourRequestDTO>();
            UpdateRequests(); 
        }
        public void UpdateRequests()
        {
            TourRequests.Clear();
            TourRequestService.ChangeStatus();
            foreach (TourRequest tourRequest in TourRequestService.GetComplexTourPart(0))
            {
                tourRequest.Location = LocationService.Get(tourRequest.Location.Id);
                tourRequest.Language = LanguageService.Get(tourRequest.Language.Id);
                TourRequests.Add(new TourRequestDTO(tourRequest));
            }
        }
        private void CreateRequest(object parameter)
        {
            frame2.Navigate(new CreateTourRequestView(User,this,frame2));
        }
        private void ComplexTour(object parameter)
        {
            frame.Navigate(new ComplexTourRequestView(User, frame, frame2));
        }
        private void Menu(object parameter)
        {
            frame.Navigate(new TouristHomeView(User, frame, frame2));

        }
        private void Statistics(object parameter)
        {
           frame.Navigate(new TourRequestStatistics(User,frame, frame2));
                 
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
