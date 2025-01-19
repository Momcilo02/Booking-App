using BookingApp.Application.UseCases;
using BookingApp.Commands;
using BookingApp.Domain.Models;
using BookingApp.DTO;
using BookingApp.View.Tourist;
using BookingApp.WPF.View.Tourist;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModel.Tourist
{
    public class ActiveTourViewModel
    {
        public TourDTO activeTour { get; set; }
        public ObservableCollection<CheckPointDTO> Checkpoints { get; set; }
        public TourReservationDTO reservedTour { get; set; }
        public TourReservationService tourReservationService { get; set; }
        public TourGuestService tourGuestService { get; set; }
        public CheckPointService CheckPointService { get; set; }
        public TourService tourService { get; set; }
        public ICommand MenuClick { get; }
        public ICommand ReservedToursClick { get; }
        public int CurrentCheckpointId
        {
            get { return reservedTour.CheckPointId; }
        }
        public Frame frame;
        public Frame frame2;
        public User User;
        public ActiveTourViewModel(User user, Frame fr, Frame frame2)
        {
            frame = fr;
            User = user;
            activeTour = new TourDTO();
            Checkpoints = new ObservableCollection<CheckPointDTO>();
            tourGuestService = new TourGuestService();
            reservedTour = new TourReservationDTO();
            MenuClick = new RelayCommand(param => Menu(param));
            ReservedToursClick = new RelayCommand(param => ReservedTours(param));
            tourReservationService = new TourReservationService();
            CheckPointService = new CheckPointService();
            tourService = new TourService();
            LoadActiveTour();
            AddTourGuests();
            AddCheckpoints();
            this.frame2 = frame2;
        }
        private void Menu(object parameter)
        {
            frame.Navigate(new TouristHomeView(User, frame, frame2));
        }
        private void ReservedTours(object parameter)
        {
            frame.Navigate(new ReservedToursView(User, frame, frame2));
        }
        public void LoadActiveTour()
        {
            foreach (TourReservation tourReservation in tourReservationService.GetAll())
            {
                if (tourReservation.IsActive == 1)
                {
                    Tour tour = tourService.Get(tourReservation.TourId);
                    activeTour = new TourDTO(tour);
                    reservedTour = new TourReservationDTO(tourReservation);
                }
            }
        }
        public void AddTourGuests()
        {
            ObservableCollection<TourGuestDTO> tourGuests = new ObservableCollection<TourGuestDTO>();
            foreach (TourGuest tourGuest in tourGuestService.GetAll())
            {
                if (tourGuest.Presence == true)
                {
                    tourGuests.Add(new TourGuestDTO(tourGuest));
                }
            }
            activeTour.TourGuests = tourGuests;
        }
        public void AddCheckpoints()
        {
            foreach (CheckPoint checkPoints in CheckPointService.GetAll())
            {
                if (checkPoints.TourId == activeTour.Id)
                {
                    Checkpoints.Add(new CheckPointDTO(checkPoints));
                }
            }
        }
        public string CurrentCheckpointDetails
        {
            get { return GetCheckpointDetails(reservedTour.CheckPointId); }
        }

        private string GetCheckpointDetails(int checkpointID)
        {

            CheckPointService checkPointService = new CheckPointService();
            CheckPoint checkPoint = checkPointService.GetCheckPointByTourAndId(activeTour.Id, checkpointID);

            if (checkPoint != null)
            {

                return $"{checkPoint.Name}";
            }
            else
            {

                return "The tour hasn't started yet.";
            }
        }

    }
}
