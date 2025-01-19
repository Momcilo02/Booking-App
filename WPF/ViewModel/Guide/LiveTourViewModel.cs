using BookingApp.Application.UseCases;
using BookingApp.Commands;
using BookingApp.Domain.Models;
using BookingApp.DTO;
using BookingApp.DTO.TourDTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModel.Guide
{
    public class LiveTourViewModel : INotifyPropertyChanged
    {

        private LiveTourDTO _liveTourDto;

        public LiveTourDTO LiveTourDto
        {
            get { return _liveTourDto; }
            set
            {
                if (_liveTourDto != value)
                {
                    _liveTourDto = value;
                    OnPropertyChanged(nameof(LiveTourDto));
                }
            }
        }

        private CheckPointDTO _selectedCheckpoint;
        public CheckPointDTO SelectedCheckpoint
        {
            get => _selectedCheckpoint;
            set
            {
                if (_selectedCheckpoint != value)
                {
                    _selectedCheckpoint = value;
                    OnPropertyChanged(nameof(SelectedCheckpoint));
                }
            }
        }

        public ObservableCollection<TourGuestDTO> TourGuestDTOs { get; set; }
        public ObservableCollection<CheckPointDTO> CheckPointDTOs { get; set; }


        public List<TourReservation> TourReservatations { get; set; }
        public TourListItem TourListItem { get; set; }

        private TourService _tourService { get; set; }
        private CheckPointService _checkPointService { get; set; }
        private LanguageService _languageService { get; set; }
        private LocationService _locationService { get; set; }
        private TourTimeService _tourTimeService { get; set; }
        private TourGuestService _tourGuestService { get; set; }
        private TourReservationService _tourReservationService { get; set; }
        private LiveTourService _liveTourService { get; set; }


        public ICommand AddCheckpointCommand { get; private set; }
        public ICommand EndTourCommand { get; private set; }



        private void InitializeServices()
        {
            _tourService = new TourService();
            _languageService = new LanguageService();
            _checkPointService = new CheckPointService();
            _locationService = new LocationService();
            _tourTimeService = new TourTimeService();
            _tourGuestService = new TourGuestService();
            _tourReservationService = new TourReservationService();
            _liveTourService = new LiveTourService();

            AddCheckpointCommand = new RelayCommand(AddCheckpoint);
            EndTourCommand = new RelayCommand(EndLiveTour);
        }

        public event Action RequestClose;

        public LiveTourViewModel(TourListItem tourItem)
        {
            InitializeServices();
            TourGuestDTOs = new ObservableCollection<TourGuestDTO>();
            CheckPointDTOs = new ObservableCollection<CheckPointDTO>();
            TourReservatations = new List<TourReservation>();


            TourListItem = tourItem;
            LiveTourDto = new LiveTourDTO
            {
                TourId = tourItem.Id,
                TourTimeId = tourItem.TourTimeId
            };

            GetTourCheckPoints();
            SelectedCheckpoint = CheckPointDTOs.FirstOrDefault();
            GetTourGuests();
        }


        private void EndLiveTour(object obj)
        {
            LiveTourDto.IsFinished = 1;
            var liveTour = LiveTourDto.ToLiveTour();
            liveTour = _liveTourService.Save(liveTour);
            foreach (var tourReservation in TourReservatations)
            {
                tourReservation.LiveTourId = liveTour.Id;
                _tourReservationService.Update(tourReservation);
            }
            foreach (var tourGuestDto in LiveTourDto.TourGuestDtos)
            {
                var tourGuest = tourGuestDto.ToTourGuest();
                _tourGuestService.Update(tourGuest);
            }

            OnRequestClose();
        }

        protected virtual void OnRequestClose()
        {
            RequestClose?.Invoke();
        }




        private void AddCheckpoint(object obj)
        {

            foreach (var guest in TourGuestDTOs.Where(g => g.IsSelected))
            {
                guest.CheckPointId = SelectedCheckpoint.Id;
                _tourGuestService.Update(guest.ToTourGuest());

                var reservation = _tourReservationService.GetAll().FirstOrDefault(x => x.Id == guest.TourReservationId);
                reservation.CheckPointId = SelectedCheckpoint.Id;
                _tourReservationService.Update(reservation);
            }


            CheckPointDTOs.Remove(SelectedCheckpoint);
            SelectedCheckpoint = CheckPointDTOs.FirstOrDefault();
            RemoveSelectedGuests();
            if (SelectedCheckpoint == null)
            {
                EndTourCommand.Execute(null);
            }
        }

        public void RemoveSelectedGuests()
        {
            var selectedGuests = TourGuestDTOs.Where(g => g.IsSelected).ToList();
            foreach (var selectedGuest in selectedGuests)
            {
                LiveTourDto.TourGuestDtos.Add(selectedGuest);
                TourGuestDTOs.Remove(selectedGuest);
            }
        }

        private void GetTourGuests()
        {
            var allReservations = _tourReservationService.GetAll();
            var tourReservations = allReservations.Where(x => x.TourId == TourListItem.Id && x.TourTimeId == TourListItem.TourTimeId).ToList();
            TourReservatations = tourReservations;
            var tourReservationIds = tourReservations.Select(x => x.Id).ToList();

            var allGuests = _tourGuestService.GetAll();

            var tourGuests = allGuests.Where(x => tourReservationIds.Contains(x.TourReservationId)).ToList();
            var tourGuestsDtos = tourGuests.Select(x => new TourGuestDTO(x)).ToList();
            tourGuestsDtos.ForEach(x => TourGuestDTOs.Add(x));
        }

        private void GetTourCheckPoints()
        {
            var checkpoints = _checkPointService.GetAll();
            var tourCheckpoints = checkpoints.Where(x => x.TourId == TourListItem.Id).ToList();
            var tourCheckpointDtos = tourCheckpoints.Select(x => new CheckPointDTO(x)).ToList();
            tourCheckpointDtos.ForEach(x => CheckPointDTOs.Add(x));
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
