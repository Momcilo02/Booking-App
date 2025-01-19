using BookingApp.Application.UseCases;
using BookingApp.Domain.Models;
using BookingApp.DTO;
using BookingApp.Repository;
using BookingApp.WPF.View.Owner;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BookingApp.View.Owner
{
    /// <summary>
    /// Interaction logic for OwnerMainView.xaml
    /// </summary>
    public partial class OwnerMainView : Window
    {
        //private User _user;
        public ObservableCollection<AccommodationReviewDTO> _reviws { get; set; }
        private GuestReviewService _guestReviewService;
        public ObservableCollection<AccommodationReviewDTO> _reviwsToShow { get; set; }
        private AccommodationReviewService _reviewService;
        private UserRepository _userRep;
        public User User { get; set; }
        public ObservableCollection<AccommodationReservationDTO> accommodationReservationDTOsToGrade { get; set; }
        public ObservableCollection<AccommodationReservation> accommodationReservationsToGrade { get; set; }

        public AccommodationReviewDTO selectedReview { get; set; }
        public AccommodationReservationDTO selectedReservation { get; set; }
        public List<AccommodationReservation> reservations { get; set; }
        public List<int> gradedReservations { get; set; }
        private AccommodationRepository accommodationRepository { get; set; }
        private AccommodationReservationRepository accommodationReservationRepository { get; set; }
        private GuestReviewRepository guestReviewRepository { get; set; }



        public OwnerMainView(User user)
        {
            
            InitializeComponent();
            DataContext = this;
            _userRep = new UserRepository();
            _reviewService = new AccommodationReviewService();
            _reviws = new ObservableCollection<AccommodationReviewDTO>();
            _reviwsToShow = new ObservableCollection<AccommodationReviewDTO>();
            _guestReviewService = new GuestReviewService();
            //_user = user;
            User = user;
           
            accommodationReservationDTOsToGrade = new ObservableCollection<AccommodationReservationDTO>();
            accommodationRepository = new AccommodationRepository();
            accommodationReservationRepository = new AccommodationReservationRepository();
            reservations = new List<AccommodationReservation>();
            accommodationReservationsToGrade = new ObservableCollection<AccommodationReservation>();
            selectedReservation = new AccommodationReservationDTO();
            selectedReview = new AccommodationReviewDTO();
            guestReviewRepository = new GuestReviewRepository();
            gradedReservations = new List<int>();
            StudentsListView.Items.Clear();
            Update();
            CalculateStatus();
            ReviewsToDisplay();

            if (accommodationReservationDTOsToGrade.Count != 0) 
            {
                MessageBox.Show("Reservations");
            }

            
        }
        private void CalculateStatus()
        {
            double grade = _reviewService.CalculateAvgGradeForOwner(User.Id);
            if (grade > 4.5 && _reviewService.GetReviewsForOwner(User.Id).Count > 50)
            {
                User.Type = Enumeration.UserType.SuperOwner;
                _userRep.Update(User);
                MessageBox.Show("Super Owner");
            }
            else
            {
                User.Type = Enumeration.UserType.Owner;
                _userRep.Update(User);
                MessageBox.Show("Owner");
            }

        }
        public void ReviewsToDisplay()
        {

            _reviwsToShow.Clear();
            foreach (var review in _reviewService.GetReviewsForOwner(User.Id))
            {
                review.Guest = _userRep.Get(review.Guest.Id);
                _reviwsToShow.Add(new AccommodationReviewDTO(review));


            }



        }

        private void OpenAddAccommodationView_Click(object sender, RoutedEventArgs e)
        {
            AddAccommodationView addAccommodationView = new AddAccommodationView(User);
            addAccommodationView.ShowDialog();
        }
        private void Accomos(object sender, RoutedEventArgs e) 
        {
            AccommodationMainView acc = new AccommodationMainView(User);
            acc.ShowDialog();
        }
        private void OpenReservationRequest_Click(object sender, RoutedEventArgs e)
        { 
            ReservationRequests reservationRequests = new ReservationRequests();
            reservationRequests.ShowDialog();
        }
        private void OpenStatus_Click(object sender, RoutedEventArgs e)
        {
            AccommodationsReviewsAndStatus status = new AccommodationsReviewsAndStatus(User);
            status.ShowDialog();
        }
        private void Update()
        {
            
            gradedReservations.Clear();
            accommodationReservationsToGrade.Clear();
            reservations.Clear();
            accommodationReservationDTOsToGrade.Clear();
            foreach (Accommodation accommodation in accommodationRepository.GetAccommodationsByOwnerId(User.Id)) 
            {
                putReservations(accommodationReservationRepository.GetByAccommodation(accommodation));
            }
            accommodationReservationsToGrade = new ObservableCollection<AccommodationReservation>(accommodationReservationRepository.GetReservationsWithinNDays(5, reservations));
            isAnyGraded();
            findAccommodationObjects();
            convertToDTOs(accommodationReservationsToGrade);
        }
        private void findAccommodationObjects() 
        {
            foreach (var reservation in accommodationReservationsToGrade) 
            {
                reservation.Accommodation = accommodationRepository.GetAccommodation(reservation.Accommodation.Id);
                accommodationReservationRepository.Update(reservation);
            }
        }
        private void putReservations(List<AccommodationReservation> reservations)
        {

            foreach (AccommodationReservation accommodationReservation in reservations)
            {
                this.reservations.Add(accommodationReservation);
            }
        }
        private void convertToDTOs(ObservableCollection<AccommodationReservation> accommodationReservations)
        {
            foreach (AccommodationReservation accommodationReservation in accommodationReservations)
            {
                accommodationReservationDTOsToGrade.Add(new AccommodationReservationDTO(accommodationReservation));
            }
        }

        private void Grade_Click(object sender, RoutedEventArgs e)
        {
            AccommodationReservationView accommodationReservationView = new AccommodationReservationView(selectedReservation);
            accommodationReservationView.ShowDialog();
            Update();
        }
        private void Detail_Click(object sender, RoutedEventArgs e)
        {
            Details Det = new Details(selectedReview);
            Det.ShowDialog();
        }
        private void isAnyGraded()
        {
            findGradedReservations();

            var copiedList = new List<AccommodationReservation>(accommodationReservationsToGrade);
                foreach (AccommodationReservation accommodationReservation in copiedList)
                {
                    if (accommodationReservationsToGrade != null)
                    {
                        if (gradedReservations.Contains(accommodationReservation.Id))
                        {
                            accommodationReservationsToGrade.Remove(accommodationReservation);
                        }
                    }
                    else 
                    {
                        break;
                    }
                }
            
        }
        private void findGradedReservations()
        {
            foreach (GuestReview guestReview in guestReviewRepository.GetAll())
            {
                gradedReservations.Add(guestReview.AccommodationReservation.Id);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }   
}
