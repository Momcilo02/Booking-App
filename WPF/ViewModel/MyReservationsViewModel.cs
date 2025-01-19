using BookingApp.Commands;
using BookingApp.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using BookingApp.View.Guest;
using BookingApp.Domain.Models;
using BookingApp.Application.UseCases;

namespace BookingApp.WPF.ViewModel
{
    public class MyReservationsViewModel : BindableBase
    {

        public User User { get; set; }
        private string title;
        public string Title
        {
            get { return title; }
            set {
                if (title != value)
                {
                    title = value;
                    OnPropertyChanged(nameof(Title));
                }
            }
        }
        public ObservableCollection<AccommodationDTO> accommodations { get; set; }
        private AccommodationReservationDTO selectedActiveReservation;

        public AccommodationReservationDTO SelectedActiveReservation 
        {
            get { return  selectedActiveReservation; }
            set
            {
                if (selectedActiveReservation != value)
                {
                    selectedActiveReservation = value;
                    OnPropertyChanged(nameof(SelectedActiveReservation));
                }
            }
        }
        private AccommodationReservationDTO selectedPastReservation;
        public AccommodationReservationDTO SelectedPastReservation
        {
            get { return selectedPastReservation; }
            set
            {
                if (selectedPastReservation != value)
                {
                    selectedPastReservation = value;
                    OnPropertyChanged(nameof(SelectedPastReservation));
                }
            }
        }
        public List<AccommodationReview> Reviews { get; set; }
        private AccommodationReservationService _reservationService;
        private AccommodationService _accommodationService;
        private AccommodationReviewService _reviewService;
        private ImageService _imageService;
        private AccommodationReservationRequestService _requestService;
        private ObservableCollection<AccommodationReservationDTO> pastReservations; 
        public ObservableCollection<AccommodationReservationDTO> PastReservations
        {
            get { return pastReservations; }
            set
            {
                pastReservations = value;
                OnPropertyChanged(nameof(PastReservations));
            }
        }
        public ICommand CancelClick { get; }
        public ICommand ReviewClick { get; }
        public ICommand ChangeReservationClick { get; }
        public ICommand DetailsPastClick { get; }
        public ICommand DetailsClick { get; }
        public MyReservationsViewModel(){
            User = new User();
            Title = "ROKAMOOOO";
            SelectedActiveReservation = new AccommodationReservationDTO();
            SelectedPastReservation = new AccommodationReservationDTO();
            ActiveReservations = new ObservableCollection<AccommodationReservationDTO>();
            PastReservations = new ObservableCollection<AccommodationReservationDTO>();
            _reservationService = new AccommodationReservationService();
            _accommodationService = new AccommodationService();
            _reviewService = new AccommodationReviewService();
            _requestService = new AccommodationReservationRequestService();
            _imageService = new ImageService();
            accommodations = new ObservableCollection<AccommodationDTO>();
            CancelClick = new RelayCommand(param => Cancel(param));
            ReviewClick = new RelayCommand(param => Review(param));
            ChangeReservationClick = new RelayCommand(param => ChangeReservation(param));
            DetailsClick = new RelayCommand(param => AccommodationDetails(param));
            DetailsPastClick = new RelayCommand(param => AccommodationPastDetails(param));
        }
        public MyReservationsViewModel(User user)
        {
            User = user;
            SelectedActiveReservation = new AccommodationReservationDTO();
            SelectedPastReservation = new AccommodationReservationDTO();
            ActiveReservations = new ObservableCollection<AccommodationReservationDTO>();
            PastReservations = new ObservableCollection<AccommodationReservationDTO>();
            _reservationService = new AccommodationReservationService();
            _accommodationService = new AccommodationService();
            _reviewService = new AccommodationReviewService();
            _requestService = new AccommodationReservationRequestService();
            _imageService = new ImageService();
            accommodations = new ObservableCollection<AccommodationDTO>();
            CancelClick = new RelayCommand(param => Cancel(param));
            ReviewClick = new RelayCommand(param => Review(param));
            ChangeReservationClick = new RelayCommand(param => ChangeReservation(param));
            UpdateReviews();
            Update();
            
        }
        private void AccommodationDetails(object parameter)
        {
            AccommodationDetailsView accommodationDetailsView = new AccommodationDetailsView(new AccommodationDTO( _accommodationService.Get((int)parameter)), User, false);
            accommodationDetailsView.ShowDialog();
        }
        private void AccommodationPastDetails(object parameter)
        {
            AccommodationDetailsView accommodationDetailsView = new AccommodationDetailsView(new AccommodationDTO(_accommodationService.Get((int)parameter)), User, false);
            accommodationDetailsView.ShowDialog();
        }
        public void Initiate(User user)
        {
            User = user;
            UpdateReviews();
            Update();
        }

        private void UpdateReviews()
        {
            Reviews = _reviewService.GetByUser(User);
        }

        private ObservableCollection<AccommodationReservationDTO> activeReservations;
        public ObservableCollection<AccommodationReservationDTO> ActiveReservations
        {
            get { return activeReservations; }
            set
            {
                activeReservations = value;
                OnPropertyChanged(nameof(ActiveReservations));
            }
        }
        
        private void Update()
        {
            ActiveReservations.Clear();
            InitiateAccommodations();
            foreach (AccommodationReservationDTO res in _reservationService.GetActiveReservations(User.Id))
            {
                
                res.Accommodation = _accommodationService.Get(res.Accommodation.Id);
                res.Accommodation.Images = _imageService.GetByAccommodationId(res.Accommodation.Id);
                ActiveReservations.Add(res);
            }
            PastReservations.Clear();
            foreach (AccommodationReservationDTO res in _reservationService.GetPastReservations(User.Id))
            {
               
                res.Accommodation = _accommodationService.Get(res.Accommodation.Id);
                res.Accommodation.Images = _imageService.GetByAccommodationId(res.Accommodation.Id);
                PastReservations.Add(res);
            }
        }

        private void InitiateAccommodations()
        {
            accommodations.Clear();
            foreach (Accommodation accommodation in _accommodationService.GetAll())
            {
                accommodations.Add(new AccommodationDTO(accommodation));
            }
        }

        private void Cancel(object parameter)
        {
            MessageBox.Show(SelectedActiveReservation.Id.ToString());

            _reservationService.CancelReservation( new AccommodationReservationDTO(_reservationService.Get((int)parameter)));
            Update();
        }
        private void Review(object parameter)
        {
            AccommodationReservationDTO res = new AccommodationReservationDTO(_reservationService.Get((int)parameter));
            if (_reviewService.GetByUserAndAccommodation(User, res.Accommodation).Id != -1)
            {
                MessageBox.Show("You already reviewed this accommodation");
                return;
            }
            
            AccommodationReviewView window = new AccommodationReviewView(User, res.Accommodation);
            window.ShowDialog();
            UpdateReviews();
        }
        private void ChangeReservation(object parameter)
        {
            if (IsAlreadyRequested((int)parameter))
            {
                MessageBox.Show("You already made a request for this reservation");
                return;
            }
            AccommodationReservation res = _reservationService.Get((int)parameter);
            ChangeReservationView window = new ChangeReservationView(User, res);
            window.ShowDialog();
        }

        private bool IsAlreadyRequested(int resId)
        {
            foreach (AccommodationReservationRequest request in _requestService.GetAll())
            {
                if (request.OldReservation.Id == resId && request.Status == Enumeration.AccommodationReservationRquest.InProcess)
                {
                    return true;
                }
            }
            return false;
        }

        public override void Demo()
        {
            throw new NotImplementedException();
        }
    }
}
