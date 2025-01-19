using BookingApp.Application.UseCases;
using BookingApp.Commands;
using BookingApp.Domain.Models;
using BookingApp.DTO;
using BookingApp.DTO.TourDTOs;
using BookingApp.View.Guide;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xceed.Wpf.Toolkit.PropertyGrid;

namespace BookingApp.WPF.ViewModel.Guide
{
    public class EndedToursViewModel
    {
        public ObservableCollection<EndedTourDTO> EndedTourDTOs { get; set; }

        public User User { get; private set; }

        private LiveTourService _liveTourService;
        private TourService _tourService;
        private TourTimeService _tourTimeService;
        private LocationService _locationService;

        public ICommand ViewReviewsCommand { get; private set; }


        public EndedToursViewModel(User user)
        {
            _tourTimeService = new TourTimeService();
            _tourService = new TourService();
            _liveTourService = new LiveTourService();
            _locationService = new LocationService();
            ViewReviewsCommand = new RelayCommand(ViewReviews);

            User = user;
            EndedTourDTOs = new ObservableCollection<EndedTourDTO>();
            GetGuideEndedTours();
        }

        private void ViewReviews(object obj)
        {
            if (obj is EndedTourDTO endedTourDto)
            {
                var tourId = endedTourDto.TourId;
                var liveTourId = endedTourDto.Id;

                var tourReviewView = new TourReviewsView(User, liveTourId);
                tourReviewView.ShowDialog();
            }
        }

        private void GetGuideEndedTours()
        {
            var allTours = _tourService.GetAll();
            var allTimes = _tourTimeService.GetAll();

            var userTours = allTours.Where(x => x.GuideId == User.Id).ToList();

            var userTourIds = userTours.Select(x => x.Id).ToList();

            var userTimes = allTimes.Where(x => userTourIds.Contains(x.Id)).ToList();

            var userTimeIds = userTimes.Select(x => x.Id).ToList();


            var endedLiveTours = _liveTourService.GetAll().Where(x => x.IsFinished == 1).ToList();

            foreach (var endedLiveTour in endedLiveTours)
            {
                var tourData = userTours.FirstOrDefault(x => x.Id == endedLiveTour.TourId);
                if (tourData == null) continue;

                var tourTimeData = userTimes.FirstOrDefault(x => x.Id == endedLiveTour.TourTimeId);
                if(tourTimeData == null) continue;

                var location = _locationService.Get(tourData.Id);
                var endedTourDto = new EndedTourDTO
                {
                    Id = endedLiveTour.Id,
                    TourId = tourData.Id,
                    TourTimeId = tourTimeData.Id,
                    Name = tourData.Name,
                    TourImage = tourData.ImagePaths.First(),
                    Location = location.City + " " + location.Country,
                    TourDate = tourTimeData.Time
                };

                EndedTourDTOs.Add(endedTourDto);
            }
        }

    }
}
