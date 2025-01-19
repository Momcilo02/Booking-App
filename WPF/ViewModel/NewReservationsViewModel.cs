using BookingApp.Application.Injector;
using BookingApp.Application.UseCases;
using BookingApp.Commands;
using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.DTO;
using BookingApp.Repository;
using BookingApp.View.Guest;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Forms;
using BookingApp.WPF.View.Guest;

namespace BookingApp.WPF.ViewModel
{
    public class NewReservationsViewModel : BindableBase
    {
        private ObservableCollection<AccommodationDTO> _accommodations;
        public ObservableCollection<AccommodationDTO> Accommodations
        {
            get { return _accommodations; }
            set
            {
                if(value != _accommodations)
                {
                    _accommodations = value;
                    OnPropertyChanged(nameof(Accommodations));
                }
            }
        }
        private AccommodationDTO selectedAccommodation;
        public AccommodationDTO SelectedAccommodation
        {
            get { return selectedAccommodation; }
            set
            {
                if(selectedAccommodation != value)
                {
                    selectedAccommodation = value;
                    OnPropertyChanged(nameof(SelectedAccommodation));
                }
            }
        }

        private ObservableCollection<LocationDTO> locations;
        public ObservableCollection<LocationDTO> Locations
        {
            get { return locations; }
            set { 
                if(locations != value)
                {
                    locations = value;
                    OnPropertyChanged(nameof(Locations));
                }
            }
        }
        public User User { get; set; }
        private AccommodationService accommodationService;
        private LocationService locationService;
        private ImageService imageService;
        public List<Image> Images { get; set; }
        private LocationDTO selectedLocation;
        public LocationDTO SelectedLocation
        {
            get { return selectedLocation; } 
            set
            {
                if(selectedLocation != value)
                {
                    selectedLocation = value;
                    OnPropertyChanged(nameof(SelectedLocation));
                }
            }
        }
        private string accommodationName;
        public string AccommodationName
        {
            get { return accommodationName; }
            set
            {
                if( accommodationName != value)
                {
                    accommodationName = value;
                    OnPropertyChanged(nameof(AccommodationName));
                }
            }
        }
        private string maxGuest;
        public string MaxGuest
        {
            get { return maxGuest; }
            set
            {
                if( maxGuest != value)
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
                if( lengthOfStay != value)
                {
                    lengthOfStay = value;
                    OnPropertyChanged(nameof(LengthOfStay));
                }
            }
        }
        public bool IsAppartmentChecked
        {
            get; set;
        }
        public bool IsHouseChecked
        {
            get; set;
        }
        public bool IsCottageChecked
        {
            get; set;
        }

        private DateTime? endDate;
        public DateTime? EndDate
        {
            get { return endDate; }
            set { 
                if( endDate != value)
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
        private string maxGuestFlex;
        public string MaxGuestFlex
        {
            get { return maxGuestFlex; }
            set
            {
                if(maxGuestFlex != value)
                {
                    maxGuestFlex = value;
                    OnPropertyChanged(nameof(MaxGuestFlex));
                }
            }
        }
        private string lengthOfStayFlex;
        public string LengthOfStayFlex
        {
            get { return  lengthOfStayFlex; }
            set { 
                if(lengthOfStayFlex != value)
                {
                    lengthOfStayFlex = value;
                    OnPropertyChanged(nameof(LengthOfStayFlex));
                }
            }
        }
        
        public List<AccommodationReservation> PreviousReservations { get; set; }
        private ObservableCollection<AccommodationDTO> flexAccommodations;
        public ObservableCollection<AccommodationDTO> FlexAccommodations
        {
            get { return flexAccommodations; }
            set
            {
                if(flexAccommodations != value)
                {
                    flexAccommodations = value;
                    OnPropertyChanged(nameof(FlexAccommodations));
                }
            }
        }
        private AccommodationReservationService reservationService;
        public AccommodationDTO SelectedAccommodationFlex {  get; set; }
        public ICommand FlexSearchClick { get; }
        public ICommand ReserveClick { get; }
        public ICommand SearchClick { get; }
        public ICommand DetailsClick { get; }
        public ICommand AllAvailableDatesClick { get; }
        public List<DateOnly> StartCandidates { get; set; }
        public List<DateOnly> EndCandidates { get; set; }
        public NewReservationsViewModel()
        {
            User = new User();
            accommodationService = new AccommodationService();
            locationService = new LocationService();
            imageService = new ImageService();
            reservationService = new AccommodationReservationService();
            Accommodations = new ObservableCollection<AccommodationDTO>();
            Locations = new ObservableCollection<LocationDTO>();
            Images = new List<Image>();
            ReserveClick = new RelayCommand(param => Reserve(param));
            DetailsClick = new RelayCommand(param => AccommodationDetails(param));
            SearchClick = new RelayCommand(param => Search(param));
            AllAvailableDatesClick = new RelayCommand(param => ShowAvailableDates(param));
            FlexSearchClick = new RelayCommand(param => FlexSearch(param));
            StartCandidates = new List<DateOnly>();
            EndCandidates = new List<DateOnly>();
            PreviousReservations = new List<AccommodationReservation>();
            FlexAccommodations = new ObservableCollection<AccommodationDTO>();
        }
        public void Initiate(User user)
        {
            User = user;
            InitializeImages();
            InitializeLocation();
            Update();
        }
        
        private void FlexSearch(object parameter)
        {
            PrepareData();
            int length;
            int maxGuest = Convert.ToInt32(MaxGuestFlex);
            int minRes = Convert.ToInt32(LengthOfStayFlex);
            Int32.TryParse(LengthOfStayFlex, out length);
            FlexAccommodations.Clear();
            if (StartDate == null || EndDate == null) { 
                SearchWithoutDates(maxGuest, minRes);
            }
            else
            {
                DateTime start = (DateTime)StartDate;
                DateTime end = (DateTime)EndDate;
                if (CheckUserInput(start, end, minRes))
                {
                    foreach (Accommodation accommodation in accommodationService.GetAll())
                    {
                        PreviousReservations = reservationService.GetAllReservationsForAccommodation(accommodation.Id);
                        if (FindFreeDatesInRange(start, end, minRes) && accommodation.MaxGuests >= maxGuest && accommodation.MinReservationDays <= minRes)
                        {
                            accommodation.Location = locationService.Get(accommodation.Location.Id);
                            AccommodationDTO acc = new AccommodationDTO(accommodation);
                            acc.Images = imageService.GetByAccommodationId(accommodation.Id);
                            //if(acc.Images.Count > 0 && acc.Images.)
                            //{
                            //    acc.MainImagePath = acc.Images[0].Path;
                            //}
                            FlexAccommodations.Add(acc);
                        }
                    }
                }
            }
        }

        private void SearchWithoutDates(int maxGuest, int minRes)
        {
            foreach (Accommodation accommodation in accommodationService.GetAll())
            {
                PreviousReservations = reservationService.GetAllReservationsForAccommodation(accommodation.Id);
                if ( accommodation.MaxGuests >= maxGuest && accommodation.MinReservationDays <= minRes)
                {
                    accommodation.Location = locationService.Get(accommodation.Location.Id);
                    AccommodationDTO acc = new AccommodationDTO(accommodation);
                    acc.Images = imageService.GetByAccommodationId(accommodation.Id);
                    //if(acc.Images.Count > 0 && acc.Images.)
                    //{
                    //    acc.MainImagePath = acc.Images[0].Path;
                    //}
                    FlexAccommodations.Add(acc);
                }
            }
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
        private bool CheckUserInput(DateTime start, DateTime end, int length)
        {
            if (start > end)
            {
                MessageBox.Show("End Date needs to be bigger then start date");
                return false;
            }
            if (length > (end - start).Days)
            {
                MessageBox.Show("Length of stay too long for selected days");
                return false;
            }
            return true;
        }
        private void PrepareData()
        {
            StartCandidates.Clear();
            EndCandidates.Clear();
        }
        private void ShowAvailableDates(object parameter)
        {
            Accommodation acc = accommodationService.Get((int)parameter);
            if (acc != null) {
                int length;
                Int32.TryParse(LengthOfStayFlex, out length);
                StartCandidates.Clear();
                EndCandidates.Clear();
                PreviousReservations = reservationService.GetAllReservationsForAccommodation(acc.Id);
                if (StartDate == null || endDate == null) {
                    FindDatesOutRange(DateTime.Now, length);
                }
                else
                {
                    DateTime start = StartDate ?? DateTime.Now;
                    DateTime end = EndDate ?? DateTime.Now;
                    FindFreeDatesInRange(start, end, length);
                }
                string message = "Chose one of the dates below";
                AvailableReservationDatesView availableReservationDatesView = new AvailableReservationDatesView(message, StartCandidates, EndCandidates, User, acc, MaxGuestFlex);
                availableReservationDatesView.ShowDialog();
                
            }
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
        private void Search(object parameter)
        {
            
            SearchAccommodationParams searchParams = GetSearchAccommodationParams();
            Accommodations.Clear();
            foreach (Accommodation accommodation in GetSuitableAccommodations(searchParams))
            {
                accommodation.Location = locationService.Get(accommodation.Location.Id);
                AccommodationDTO acc = new AccommodationDTO(accommodation);
                acc.Images = imageService.GetByAccommodationId(accommodation.Id);
                //if(acc.Images.Count > 0 && acc.Images.)
                //{
                //    acc.MainImagePath = acc.Images[0].Path;
                //}
                Accommodations.Add(acc);
            }
        }
        
        private SearchAccommodationParams GetSearchAccommodationParams()
        {
            int maxGuests;
            if (int.TryParse(MaxGuest, out maxGuests) == false)
            {
                maxGuests = -1;
            }
            int lenghtOfStay;
            if (int.TryParse(LengthOfStay, out lenghtOfStay) == false)
            {
                lenghtOfStay = -1;
            }
            return new SearchAccommodationParams(AccommodationName, GetSuitableTypes(), SelectedLocation.Id, maxGuests, lenghtOfStay);
        }
        private List<Enumeration.AccommodationType> GetSuitableTypes()
        {
            List<Enumeration.AccommodationType> types = new List<Enumeration.AccommodationType>();
            if (IsAppartmentChecked == true)
            {
                types.Add(Enumeration.AccommodationType.Apartment);
            }
            if (IsHouseChecked == true)
            {
                types.Add(Enumeration.AccommodationType.House);
            }
            if (IsCottageChecked == true)
            {
                types.Add(Enumeration.AccommodationType.Cottage);
            }

            return types;
        }
        private bool IsAccommodationSuitable(Accommodation accommodation, SearchAccommodationParams searchParams)
        {
            if (!IsTypeSuitable(accommodation, searchParams.Types) ||
                !IsNameSuitable(accommodation, searchParams.Name) ||
                !IsLocationSuitable(accommodation, searchParams.LocationId) ||
                !IsMaxGuestsSuitable(accommodation, searchParams.MaxGuests) ||
                !IsLengthOfStaySuitable(accommodation, searchParams.StayLength))
            {
                return false;
            }

            return true;
        }
        private bool IsTypeSuitable(Accommodation accommodation, List<Enumeration.AccommodationType> types)
        {
            return (types.Count == 0 || types.Contains(accommodation.Type));
        }

        private bool IsNameSuitable(Accommodation accommodation, string name)
        {
            return (string.IsNullOrEmpty(name) || accommodation.Name.ToLower().Contains(name.ToLower()));
        }

        private bool IsLocationSuitable(Accommodation accommodation, int locationId)
        {
            return (locationId == -1 || locationId == accommodation.Location.Id);
        }

        private bool IsMaxGuestsSuitable(Accommodation accommodation, int maxGuests)
        {
            return (maxGuests == -1 || maxGuests <= accommodation.MaxGuests);
        }

        private bool IsLengthOfStaySuitable(Accommodation accommodation, int lengthOfStay)
        {
            return (lengthOfStay == -1 || lengthOfStay >= accommodation.MinReservationDays);
        }
        private List<Accommodation> GetSuitableAccommodations(SearchAccommodationParams searchParams)
        {
            List<Accommodation> suitableAccommodations = new List<Accommodation>();
            foreach (Accommodation accommodation in accommodationService.GetAll())
            {
                if (IsAccommodationSuitable(accommodation, searchParams))
                {
                    suitableAccommodations.Add(accommodation);
                }
            }
            return suitableAccommodations;
        }
        private void AccommodationDetails(object parameter)
        {
            AccommodationDetailsView accommodationDetailsView = new AccommodationDetailsView(new AccommodationDTO(accommodationService.Get((int)parameter)), User, true);
            accommodationDetailsView.ShowDialog();
        }
        private void Reserve(object parameter)
        {
            int id = (int)parameter;
            Accommodation accommodation = accommodationService.Get(id);
            ReserveAccommodationView reserveAccommodationView = new ReserveAccommodationView(accommodation, User);
            reserveAccommodationView.ShowDialog();
        }

        public void InitializeImages()
        {
            Images.Clear();
            foreach(Image image in imageService.GetAll())
            {
                Images.Add(image);
            }
        }
        public void InitializeLocation()
        {
            Location empty = new Location(-1, "", "");
            
            Locations.Clear();
            Locations.Add(new LocationDTO(empty));
            foreach(Location location in locationService.GetAll())
            {
                Locations.Add(new LocationDTO(location));
            }
            SelectedLocation = new LocationDTO(empty);

        }
        public void Update()
        {
            Accommodations.Clear();
            foreach(Accommodation accommodation in accommodationService.GetAll())
            {
                accommodation.Location = locationService.Get(accommodation.Location.Id);
                AccommodationDTO acc = new AccommodationDTO(accommodation);
                acc.Images = imageService.GetByAccommodationId(accommodation.Id);
                //if(acc.Images.Count > 0 && acc.Images.)
                //{
                //    acc.MainImagePath = acc.Images[0].Path;
                //}
                Accommodations.Add(acc);
            }
            
        }

        public override void Demo()
        {
            throw new NotImplementedException();
        }
    }
}
