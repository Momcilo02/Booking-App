using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BookingApp.View.Guest;
using System.Collections.ObjectModel;
using BookingApp.DTO;
using BookingApp.Domain.Models;
using BookingApp.Application.UseCases;
using System.Security.RightsManagement;
using System.Windows.Input;
using BookingApp.Commands;
using BookingApp.WPF.View.Guest;


namespace BookingApp.WPF.ViewModel
{
    public class GuestProfileViewModel : BindableBase
    {
        
        public User? User { get; set; }
        public bool IsSuperGuest { get; set; }
        public int SuperPointsRemaining { get; set; }
        
        private AccommodationReservationRequestService _requestService;
        private AccommodationReservationService _reservationService;
        private SuperGuestService _superGuestService;
        private AccommodationService _accommodationService;
        private GuestReviewService _guestReviewService;

        private string userPassword;
        public string UserPassword 
        {
            get { return userPassword; }

            set
            {
                if(userPassword != value)
                {
                    userPassword = value;
                    OnPropertyChanged(nameof(UserPassword));
                }
            }
        }
        public bool isPasswordStars {  get; set; }
        private List<Accommodation> Accommodations { get; set; }
        private List<AccommodationReservation> Reservations { get; set; }

        public List<User> Users { get; set; }

        private ObservableCollection<AccommodationReservationRequestDTO> requests;
        public ObservableCollection<AccommodationReservationRequestDTO> Requests
        {
            get { return requests; }
            set
            {
                requests = value;
                OnPropertyChanged(nameof(Requests));
            }
        }
        private ObservableCollection<GuestReviewDTO> guestReviews;
        public ObservableCollection<GuestReviewDTO> GuestReviews
        {
            get { return guestReviews; }
            set
            {
                if( guestReviews != value)
                {
                    guestReviews = value;
                    OnPropertyChanged(nameof(GuestReviews));
                }
            }
        }

        public ICommand ShowPasswordClick { get; }
        public ICommand RequestDetailsClick { get; }

        public GuestProfileViewModel()
        {

        }

        public GuestProfileViewModel(User guest)
        {
            User = guest;
            InitiatePassword();
            RequestDetailsClick = new RelayCommand(param => RequestDetails(param));
            _requestService = new AccommodationReservationRequestService();
            _reservationService = new AccommodationReservationService();
            _accommodationService = new AccommodationService();
            _superGuestService = new SuperGuestService();
            _guestReviewService = new GuestReviewService();
            Accommodations = new List<Accommodation>();
            Reservations = new List<AccommodationReservation>();
            Requests = new ObservableCollection<AccommodationReservationRequestDTO>();
            ShowPasswordClick = new RelayCommand(param => ShowPassword(param));
            GuestReviews = new ObservableCollection<GuestReviewDTO>();
            IsSuperGuest = _superGuestService.IsSuperGuest(User.Id);
            SuperPointsRemaining = _superGuestService.GetRemainingPoints(User.Id);
            Update();
        }
        private void RequestDetails(object parametar)
        {
            int id = (int)parametar;
            AccommodationReservationRequestDTO requestDTO = new AccommodationReservationRequestDTO();
            foreach (AccommodationReservationRequestDTO request in Requests)
            {
                if (request.Id == id)
                {
                    requestDTO = request;
                }
            }
            if (requestDTO != null) { 
                RequestDetailsView requestDetailsView = new RequestDetailsView(requestDTO);
                requestDetailsView.ShowDialog();
            }
        }
        private void ShowPassword(object parametar)
        {
            if (isPasswordStars)
            {
                UserPassword = User.Password;
                isPasswordStars = false;
            }
            else
            {
                InitiatePassword();
            }
        }
        private void InitiatePassword()
        {
            UserPassword = string.Empty;
            foreach(char c in User.Password)
            {
                UserPassword += "*";
            }
            
            isPasswordStars = true;
        }


        private void Update()
        {
            UpdateReservations();
            Requests.Clear();
            foreach (AccommodationReservationRequest item in _requestService.GetAll())
            {
                
                item.OldReservation = Reservations.Find(r => r.Id == item.OldReservation.Id);
                AccommodationReservationRequestDTO requestDTO = new AccommodationReservationRequestDTO(item);
                requestDTO.StatusImage = GetStatusImagePath(item);
                Requests.Add(requestDTO);

            }
            GuestReviews.Clear();
            foreach(GuestReview guestReview in _guestReviewService.GetAllForGuest(User))
            {
                guestReview.AccommodationReservation = Reservations.Find(r => r.Id == guestReview.AccommodationReservation.Id);
                GuestReviews.Add(new GuestReviewDTO(guestReview));
            }
        }

        private string GetStatusImagePath(AccommodationReservationRequest request)
        {
            if(request.Status == Enumeration.AccommodationReservationRquest.Approved)
            {
                return "\\Resources\\Images\\requestApproved.png";
            }else if(request.Status == Enumeration.AccommodationReservationRquest.InProcess)
            {
                return "\\Resources\\Images\\InProcessRequest.png";
            }
            else
            {
                return "\\Resources\\Images\\RejectRequest.png";
            }
        }
        private void UpdateReservations()
        {
            UpdateAccommodations();
            Reservations.Clear();
            foreach (AccommodationReservation reservation in _reservationService.GetAll())
            {
                Accommodation accommodation = Accommodations.Find(acc => acc.Id == reservation.Accommodation.Id);
                reservation.Accommodation = accommodation;
                reservation.User = User;
                Reservations.Add(reservation);
            }
        }
        private void UpdateAccommodations()
        {
            foreach (Accommodation accommodation in _accommodationService.GetAll())
            {
                Accommodations.Add(accommodation);
            }
        }

        public override void Demo()
        {
            throw new NotImplementedException();
        }
    }
}
