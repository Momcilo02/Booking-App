using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BookingApp.Domain.Models;
using BookingApp.DTO;
using System.Windows;
using BookingApp.Application.UseCases;
using System.Windows.Forms;
using BookingApp.WPF.View.Guest;
using BookingApp.Commands;
using System.Text.RegularExpressions;
using BookingApp.Validation;

namespace BookingApp.WPF.ViewModel
{
    public class ReserveAccommodationViewModel:BindableBase
    {
        public User User { get; set; }
        public Accommodation Accommodation { get; set; }
        private DateTime? endDate;
        public DateTime? EndDate
        {
            get { return endDate; }
            set
            {
                if (endDate != value)
                {
                    endDate = value;
                    OnPropertyChanged(nameof(EndDate));
                }
            }
        }
        private DateTime? startDate;
        public DateTime? StartDate
        {
            get { return startDate; }
            set
            {
                if (startDate != value)
                {
                    startDate = value;
                    OnPropertyChanged(nameof(StartDate));
                }
            }
        }
        private string maxGuest;
        public string MaxGuest
        {
            get { return maxGuest; }
            set
            {
                if (maxGuest != value)
                {
                    maxGuest = value;
                    OnPropertyChanged(nameof(MaxGuest));
                }
            }
        }
        private string lengthOfStay;
        public string LengthOfStay
        {
            get { return lengthOfStay; }
            set
            {
                if (lengthOfStay != value)
                {
                    lengthOfStay = value;
                    OnPropertyChanged(nameof(LengthOfStay));
                }
            }
        }
        public List<DateOnly> StartCandidates { get; set; }
        public List<DateOnly> EndCandidates { get; set; }
        private ObservableCollection<AccommodationReservationDTO> reservations;
        public ObservableCollection<AccommodationReservationDTO> Reservations
        {
            get { return reservations; }
            set
            {
                if (reservations != value)
                {
                    reservations = value;
                    OnPropertyChanged(nameof(Reservations));
                }
            }
        }
        private AccommodationReservationDTO selectedReservation;
        public AccommodationReservationDTO SelectedReservation
        {
            get { return selectedReservation; }
            set
            {
                if (selectedReservation != value)
                {
                    selectedReservation = value;
                    OnPropertyChanged(nameof(SelectedReservation));
                }
            }
        }
        public AccommodationReservationDTO getParams = new AccommodationReservationDTO();

        public AccommodationReservationDTO GetParams
        {
            get { return getParams; }
            set
            {
                getParams = value;
                OnPropertyChanged(nameof (GetParams));
            }
        }
        public ICommand CancelClick { get; }
        public ICommand ConfirmClick { get; }
        public ICommand AddReservationClick { get; }
        private AccommodationReservationService reservationService;
        public List<AccommodationReservation> PreviousReservations { get; set; }
       
        public ReserveAccommodationViewModel()
        {
            User = new User();
            reservationService = new AccommodationReservationService();
            StartCandidates = new List<DateOnly>();
            EndCandidates = new List<DateOnly>();
            PreviousReservations = new List<AccommodationReservation>();
            Reservations = new ObservableCollection<AccommodationReservationDTO>();
            CancelClick = new RelayCommand(param => Cancel(param));
            ConfirmClick = new RelayCommand(param => Confirm(param));
            AddReservationClick = new RelayCommand(param =>  AddReservation(param));
        }
        public void Initiate(User user, Accommodation accommodation)
        {
            User = user;
            Accommodation = accommodation;
            PreviousReservations = reservationService.GetAllReservationsForAccommodation(accommodation.Id);
            GetParams.MinResDays = accommodation.MinReservationDays;
            GetParams.Accommodation = Accommodation;
        }


        private void Confirm(object parameter)
        {
            GetParams.CheckNumber = false;
            GetParams.Validate();
            if (GetParams.IsValid)
            {
                PrepareData();
                int minRes = Convert.ToInt32(GetParams.LengthOfStay);
                DateTime start = GetParams.StartDate ?? DateTime.Now;
                DateTime end = GetParams.EndDate ?? DateTime.Now;
                    if (!FindFreeDatesInRange(start, end, minRes))
                    {
                        FindDatesOutRange(start, minRes);
                        System.Windows.MessageBox.Show("No available Dates in the period you chose, here are some other free dates outside that period");
                    }
                    InitializeReservations();
                GetParams.CheckNumber = true;
            }
            else
            {
                System.Windows.MessageBox.Show("Los unos");
            }
            
        }
        private void InitializeReservations()
        {
            Reservations.Clear();
            for (int i = 0; i < StartCandidates.Count; i++)
            {
                Reservations.Add(new AccommodationReservationDTO(-1, StartCandidates[i], StartCandidates[i], User.Id, Accommodation,"0"));
            }
        }
        private bool FindFreeDatesInRange(DateTime start, DateTime end, int length)
        {
            bool notFree = false;
            while (start != end.AddDays(-(length - 1)))
            {
                foreach (AccommodationReservation reservation in PreviousReservations)
                {
                    notFree = IsDateFree(start, length, reservation);
                    if (notFree)
                    {
                        break;
                    }
                }
                if (!notFree)
                {
                    StartCandidates.Add(DateOnly.FromDateTime(start));
                    EndCandidates.Add(DateOnly.FromDateTime(start.AddDays(length)));
                }
                notFree = false;
                start = start.AddDays(1);
            }
            return StartCandidates.Count > 0;
        }
        private bool IsDateFree(DateTime start, int length, AccommodationReservation reservation)
        {
            return IsOverlap(start, length, reservation);
        }

        private bool IsOverlap(DateTime start, int length, AccommodationReservation reservation)
        {
            DateTime reservationStartDate = reservation.StartDate.ToDateTime(TimeOnly.Parse("10:00PM"));
            DateTime reservationEndDate = reservation.EndDate.ToDateTime(TimeOnly.Parse("10:00PM"));


            return (IsWithinRange(start, reservationStartDate, reservationEndDate) ||
                    IsWithinRange(start.AddDays(length), reservationStartDate, reservationEndDate) ||
                    (start < reservationStartDate && start.AddDays(length) > reservationEndDate));
        }

        private bool IsWithinRange(DateTime dateCandidate, DateTime start, DateTime end)
        {
            return (dateCandidate >= start && dateCandidate <= end);
        }
        private void FindDatesOutRange(DateTime start, int length)
        {
            while (StartCandidates.Count < 5)
            {
                FindFreeDatesInRange(start, start.AddDays(30), length);
                start = start.AddDays(30 - length);
            }
            if (StartCandidates.Count > 5)
            {
                StartCandidates.RemoveRange(5, StartCandidates.Count - 5);
                EndCandidates.RemoveRange(5, EndCandidates.Count - 5);
            }
        }
        private bool CheckUserInput(DateTime start, DateTime end, int length)
        {
            if (start > end)
            {
                System.Windows.Forms.MessageBox.Show("End Date needs to be bigger then start date");
                return false;
            }
            if (length > (end - start).Days)
            {
                System.Windows.Forms.MessageBox.Show("Length of stay too long for selected days");
                return false;
            }
            if(length < Accommodation.MinReservationDays)
            {
                System.Windows.Forms.MessageBox.Show("Length of stay too short for this accommodaton, need to be at least " + Accommodation.MinReservationDays.ToString() + " days");
                return false;
            }
            return true;
        }
        private void PrepareData()
        {
            StartCandidates.Clear();
            EndCandidates.Clear();
        }
        private void Cancel(object window)
        {
            Window win = (Window)window;
            win.Close();
        }
        private void AddReservation(object parameter)
        {
            Window win = (Window)parameter;
            GetParams.CheckNumber = true;
            GetParams.Validate();
            if (GetParams.IsValid)
            {
                if (CheckFinalParam())
                {
                    DateOnly ChosenStart = DateOnly.FromDateTime((DateTime)SelectedReservation.StartDate);
                    DateOnly ChosenEnd = DateOnly.FromDateTime((DateTime)SelectedReservation.EndDate);
                    AccommodationReservation reservation = new AccommodationReservation(Accommodation, User, ChosenStart, ChosenEnd, Convert.ToInt32(GetParams.NumberOfPeople));
                    reservationService.Save(reservation);
                    System.Windows.MessageBox.Show("You have successfully reserved an accommodation");
                    win.Close();
                }
            }
            
        }

        private bool CheckFinalParam()
        {
            if (SelectedReservation == null)
            {
                System.Windows.MessageBox.Show("You must select one date before you press confirm");
                return false;
            }
            else if (Convert.ToInt32(MaxGuest) > Accommodation.MaxGuests)
            {
                System.Windows.MessageBox.Show("Accommodation can only host " + Accommodation.MaxGuests.ToString() + " guests");
                return false;
            }
            return true;
        }

        public override void Demo()
        {
            throw new NotImplementedException();
        }
    }
}
