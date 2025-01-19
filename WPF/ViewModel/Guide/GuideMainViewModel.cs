using BookingApp.Application.UseCases;
using BookingApp.Commands;
using BookingApp.Domain.Models;
using BookingApp.DTO.TourDTOs;
using BookingApp.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModel.Guide
{
    public class GuideMainViewModel
    {
        public ObservableCollection<TourListItem> Tours { get; private set; }

        private TourService _tourService;
        private TourTimeService _tourTimeService;
        private LocationService _locationService;
        private CheckPointService _checkPointService;
        public User User { get; private set; }

        public TourGuideHelper _tourGuideHelper;

        public ICommand ViewCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }

        public GuideMainViewModel(User user)
        {
            Tours = new ObservableCollection<TourListItem>();
            ViewCommand = new RelayCommand(ViewTour);
            CancelCommand = new RelayCommand(CancelTour);
            _tourService = new TourService();
            _tourTimeService = new TourTimeService();
            _locationService = new LocationService();
            _checkPointService = new CheckPointService();
            User = user;

            _tourGuideHelper = new TourGuideHelper(_tourService, _tourTimeService, _locationService);
            _tourGuideHelper.LoadTours(Tours, user);
        }


        public void RefreshTours()
        {
            Tours.Clear();
            _tourGuideHelper.LoadTours(Tours, User);
        }


        private void ViewTour(object obj)
        {
           
        }

        private void CancelTour(object obj)
        {
            if (obj is TourListItem tourItem)
            {
                MessageBoxResult result = MessageBox.Show($"Are you sure you want to cancel the tour?",
                                                          "Confirm Cancellation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    int tourId = tourItem.Id;
                    int tourTimeId = tourItem.TourTimeId;

                    var tourToCancel = Tours.FirstOrDefault(x => x.Id == tourId && x.TourTimeId == tourTimeId);
                    
                    if (tourToCancel != null)
                    {
                        Tours.Remove(tourToCancel);
                        _tourTimeService.DeleteById(tourTimeId);
                        MessageBox.Show("Tour has been successfully canceled.", "Cancellation Successful", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
        }


    }
}
