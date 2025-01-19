using BookingApp.Application.UseCases;
using BookingApp.Domain.Models;
using BookingApp.DTO.TourDTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Helpers
{
    public class TourGuideHelper
    {
        private TourService _tourService;
        private TourTimeService _tourTimeService;
        private LocationService _locationService;
        public TourGuideHelper(TourService tourService, TourTimeService tourTimeService, LocationService locationService)
        {
            _tourService = tourService;
            _tourTimeService = tourTimeService;
            _locationService = locationService;
            
        }

        public void GetToursForStatistics(ObservableCollection<TourListItem> tourListItems, User user) 
        {
            var tours = _tourService.GetGuideTours(user.Id);
            foreach (var tour in tours)
            {
                var location = _locationService.Get(tour.Location.Id);
               
                tourListItems.Add(new TourListItem
                    {
                        Id = tour.Id,
                        TourName = tour.Name,
                        ImagePath = tour.ImagePaths.First(),
                        Location = location.City + ", " + location.Country,
                    });
                
            }

        }

        public void LoadTours(ObservableCollection<TourListItem> tourListItems, User user)
        {
            var tours = _tourService.GetGuideTours(user.Id);

            foreach (var tour in tours)
            {
                var tourTimes = _tourTimeService.GetTourTimesByTourId(tour.Id);
                var location = _locationService.Get(tour.Location.Id);
                foreach (var tourTime in tourTimes)
                {
                    tourListItems.Add(new TourListItem
                    {
                        Id = tour.Id,
                        TourTimeId = tourTime.Id,
                        TourName = tour.Name,
                        ImagePath = tour.ImagePaths.First(),
                        Location = location.City + ", " + location.Country,
                        TourDate = tourTime.Time
                    });
                }
            }
;
        }

    }
}
