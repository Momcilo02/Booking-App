using BookingApp.Application.UseCases;
using BookingApp.Commands;
using BookingApp.Domain.Models;
using BookingApp.DTO.TourDTOs;
using BookingApp.Helpers;
using BookingApp.View.Guide;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModel.Guide
{
    public class StartTourViewModel
    {
        public ObservableCollection<TourListItem> Tours { get; private set; }
        private TourService _tourService;
        private TourTimeService _tourTimeService;
        private LocationService _locationService;
        public TourGuideHelper _tourGuideHelper;
        private LiveTourService _liveTourService;
        public ICommand StartTourCommand { get; private set; }
        public event Action<UserControl> RequestNavigate;


        public User User { get; private set; }

        public StartTourViewModel(User user)
        {
            _tourService = new TourService();
            _locationService = new LocationService();
            _tourTimeService = new TourTimeService();
            _liveTourService = new LiveTourService();

            var allEndedTours = _liveTourService.GetAll().Where(x => x.IsFinished == -1).ToList();


            Tours = new ObservableCollection<TourListItem>();
            StartTourCommand = new RelayCommand(StartTour);
            User = user;

            _tourGuideHelper = new TourGuideHelper(_tourService, _tourTimeService, _locationService);
            _tourGuideHelper.LoadTours(Tours, user);
            var notTodayTours = Tours.Where(x => x.TourDate.Date != DateTime.Today).ToList();
            notTodayTours.ForEach(x => { Tours.Remove(x); });
        }


        private void StartTour(object obj)
        {
            if (obj is not TourListItem tourItem) return;

            var control = new LiveTourView(User, tourItem);
            RequestNavigate?.Invoke(control);
        }

    }
}
