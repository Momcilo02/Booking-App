using BookingApp.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
using BookingApp.Repository;
using BookingApp.Domain.Models;
using BookingApp.Application.UseCases;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Application.Injector;
using BookingApp.WPF.ViewModel;

namespace BookingApp.View.Guest
{
    /// <summary>
    /// Interaction logic for GuestMainView.xaml
    /// </summary>
    public partial class GuestMainView : Window
    {
        
        public GuestMainView(User user)
        {
            InitializeComponent();
            DataContext = new GuestMainWindowViewModel(user);
            
        }

        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        //private void CheckForRequestUpdates()
        //{
        //    bool changed = false;
        //    List<AccommodationReservationRequest> forDelete = new List<AccommodationReservationRequest>();
        //    foreach(var request in _requestService.GetAll()) {
        //        foreach (var other in _requestService.GetAll())
        //        {
        //            if(request.Status != other.Status && request.OldReservation.Id == other.OldReservation.Id && request.Status == Enumeration.AccommodationReservationRquest.InProcess)
        //            {
        //                forDelete.Add(request);
        //                changed = true;
        //                break;
        //            }
        //        }
        //    }
        //    foreach (var request in forDelete)
        //    {
        //        _requestService.Delete(request);
        //    }
        //    if(changed)
        //    {
        //        MessageBox.Show("One of your Requests has been changed. Go to profile tab");
        //    }
        //}



        //private void SearchButton_Click(object sender, RoutedEventArgs e)
        //{
        //    SearchAccommodationParams searchParams = new SearchAccommodationParams();
        //    accommodations.Clear();
        //    foreach(Accommodation accommodation in GetSuitableAccommodations(searchParams)) 
        //    {
        //        accommodation.Location = locationRepository.Get(accommodation.Location.Id);
        //        accommodations.Add(new AccommodationDTO(accommodation));
        //    }

        //}
        //private SearchAccommodationParams GetSearchAccommodationParams()
        //{
        //    int maxGuests;
        //    if (int.TryParse(MaxGuestsTextBox.Text, out maxGuests) == false)
        //    {
        //        maxGuests = -1;
        //    }
        //    int lenghtOfStay;
        //    if (int.TryParse(LenghtOfStayTextBox.Text, out lenghtOfStay) == false)
        //    {
        //        lenghtOfStay = -1;
        //    }
        //    return new SearchAccommodationParams(NameTextBox.Text, GetSuitableTypes(), selectedLocation.Id, maxGuests, lenghtOfStay);
        //}

        //private List<Accommodation> GetSuitableAccommodations(SearchAccommodationParams searchParams)
        //{
        //    List<Accommodation> suitableAccommodations = new List<Accommodation>();
        //    foreach (Accommodation accommodation in accommodationRepository.GetAll())
        //    {
        //        if(IsAccommodationSuitable(accommodation, searchParams))
        //        {
        //            suitableAccommodations.Add(accommodation);
        //        }
        //    }
        //    return suitableAccommodations;
        //}

        //private bool IsAccommodationSuitable(Accommodation accommodation, SearchAccommodationParams searchParams)
        //{
        //    if (!IsTypeSuitable(accommodation, searchParams.Types) ||
        //        !IsNameSuitable(accommodation, searchParams.Name) ||
        //        !IsLocationSuitable(accommodation, searchParams.LocationId) ||
        //        !IsMaxGuestsSuitable(accommodation, searchParams.MaxGuests) ||
        //        !IsLengthOfStaySuitable(accommodation, searchParams.StayLength))
        //    {
        //        return false;
        //    }

        //    return true;
        //}

        //private bool IsTypeSuitable(Accommodation accommodation, List<Enumeration.AccommodationType> types)
        //{
        //    return (types.Count == 0 || types.Contains(accommodation.Type));
        //}

        //private bool IsNameSuitable(Accommodation accommodation, string name)
        //{
        //    return (string.IsNullOrEmpty(name) || accommodation.Name.ToLower().Contains(name.ToLower()));
        //}

        //private bool IsLocationSuitable(Accommodation accommodation, int locationId)
        //{
        //    return (locationId == -1 || locationId == accommodation.Location.Id);
        //}

        //private bool IsMaxGuestsSuitable(Accommodation accommodation, int maxGuests)
        //{
        //    return (maxGuests == -1 || maxGuests <= accommodation.MaxGuests);
        //}

        //private bool IsLengthOfStaySuitable(Accommodation accommodation, int lengthOfStay)
        //{
        //    return (lengthOfStay == -1 || lengthOfStay >= accommodation.MinReservationDays);
        //}


        //private List<Enumeration.AccommodationType> GetSuitableTypes()
        //{
        //    List<Enumeration.AccommodationType> types = new List<Enumeration.AccommodationType>();
        //    if (ApartmentCheckBox.IsChecked == true)
        //    {
        //        types.Add(Enumeration.AccommodationType.Apartment);
        //    }
        //    if(HouseCheckBox.IsChecked == true)
        //    {
        //        types.Add(Enumeration.AccommodationType.House);
        //    }
        //    if(CottageCheckBox.IsChecked==true)
        //    {
        //        types.Add(Enumeration.AccommodationType.Cottage);
        //    }

        //    return types;
        // }

        //private void ReserveButton_Click(object sender, RoutedEventArgs e)
        //{
        //    AccommodationDetailsView detail = new AccommodationDetailsView(selectedAccommodation, Guest);
        //    detail.Owner = this;
        //    detail.WindowStartupLocation = WindowStartupLocation.CenterOwner;
        //    detail.Show();
        //}

        //private void MyReservationsButton_Click(object sender, RoutedEventArgs e)
        //{
        //    MyReservationsView myReservationsView = new MyReservationsView(Guest);
        //    myReservationsView.Owner = this;
        //    myReservationsView.WindowStartupLocation = WindowStartupLocation.CenterOwner;
        //    myReservationsView.ShowDialog();
        //}

        //private void ProfileButton_Click(object sender, RoutedEventArgs e)
        //{
        //    MessageBox.Show("krenuo");
        //    GuestProfileView guestProfileView = new GuestProfileView(Guest);
        //    guestProfileView.Owner = this;
        //    guestProfileView.WindowStartupLocation= WindowStartupLocation.CenterOwner;
        //    guestProfileView.ShowDialog();
        //}
    }
}
