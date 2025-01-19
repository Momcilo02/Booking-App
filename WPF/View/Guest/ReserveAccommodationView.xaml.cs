using BookingApp.Domain.Models;
using BookingApp.DTO;
using BookingApp.Repository;
using BookingApp.WPF.ViewModel;
using System;
using System.Collections.Generic;
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

namespace BookingApp.View.Guest
{
    /// <summary>
    /// Interaction logic for ReserveAccommodationView.xaml
    /// </summary>
    public partial class ReserveAccommodationView : Window
    {
        //public DateTime ReservationStart {  get; set; }
        //public DateTime ReservationEnd {  get; set; }
        //public List<DateOnly> StartCandidates { get; set; }
        //public List<DateOnly> EndCandidates { get; set; }
        //private AccommodationReservationRepository ReservationRepository { get; set; }
        //public List<AccommodationReservation> PreviousReservations {  get; set; }
        //public Accommodation SelectedAccommodation { get; set; }
        //public int length;
        //public User Guest {  get; set; }
        public ReserveAccommodationView(Accommodation accommodation, User user)
        {
            InitializeComponent();
            ReserveAccommodationViewModel viewModel = new ReserveAccommodationViewModel();
            viewModel.Initiate(user, accommodation);
            DataContext = viewModel;
        }

        //private void CancelButton_Click(object sender, RoutedEventArgs e)
        //{
        //    Close();
        //}

        //private void PrepareData()
        //{
        //    StartCandidates.Clear();
        //    EndCandidates.Clear();

        //}
        //private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        //{
        //    PrepareData();
        //    string message;
        //    if(CheckUserInput(ReservationStart, ReservationEnd, length))
        //    {
                
        //        if (!FindFreeDatesInRange(ReservationStart, ReservationEnd, length))
        //        {
        //            FindDatesOutRange(ReservationStart, length);
        //            message = "No Available Dates in the period you chose, here are some other free dates outside the period you chose";
        //        }
        //        else
        //        {
        //            message = "Here are all available dates.";
        //        }
        //        AvailableReservationDatesView availableReservationDatesView = new AvailableReservationDatesView(message, StartCandidates, EndCandidates, Guest, SelectedAccommodation);
        //        availableReservationDatesView.ShowDialog();
        //    }
        //}
        //private bool FindFreeDatesInRange(DateTime start, DateTime end, int length)
        //{
        //    bool notFree = false;
        //    while (start != end.AddDays(-(length - 1)))
        //    {
        //        foreach(AccommodationReservation reservation in PreviousReservations)
        //        {
        //            notFree = IsDateFree(start, length, reservation);
        //            if (notFree)
        //            {
        //                break;
        //            }
        //        }
        //        if(!notFree)
        //        {
        //            StartCandidates.Add(DateOnly.FromDateTime(start));
        //            EndCandidates.Add(DateOnly.FromDateTime(start.AddDays(length)));
        //        }
        //        notFree= false;
        //        start = start.AddDays(1);
        //    }
        //    return StartCandidates.Count > 0;
        //}
        
        //private void FindDatesOutRange(DateTime start, int length)
        //{
        //    while (StartCandidates.Count < 5)
        //    {
        //        FindFreeDatesInRange(start, start.AddDays(30), length);
        //        start = start.AddDays(30 - length);
        //    }
        //    if(StartCandidates.Count > 5) {
        //        StartCandidates.RemoveRange(5, StartCandidates.Count - 5);
        //        EndCandidates.RemoveRange(5, EndCandidates.Count - 5);
        //    } 
        //}
        //private bool CheckUserInput(DateTime start,  DateTime end, int length)
        //{
        //    if (start > end )
        //    {
        //        MessageBox.Show("End Date needs to be bigger then start date");
        //        return false;
        //    }
        //    if(length > (end - start).Days )
        //    {
        //        MessageBox.Show("Length of stay too long for selected days");
        //        return false;
        //    }
        //    if(length < SelectedAccommodation.MinReservationDays)
        //    {
        //        MessageBox.Show("Length of stay too short, needs to be at least" + SelectedAccommodation.MinReservationDays.ToString() + "days");
        //        return false;
        //    }
        //    return true;
        //}
        //private bool IsDateFree(DateTime start, int length, AccommodationReservation reservation)
        //{
        //    return IsOverlap(start, length, reservation);
        //}

        //private bool IsOverlap(DateTime start, int length, AccommodationReservation reservation)
        //{
        //    DateTime reservationStartDate = reservation.StartDate.ToDateTime(TimeOnly.Parse("10:00PM"));
        //    DateTime reservationEndDate = reservation.EndDate.ToDateTime(TimeOnly.Parse("10:00PM"));

            
        //    return (IsWithinRange(start, reservationStartDate, reservationEndDate) ||
        //            IsWithinRange(start.AddDays(length), reservationStartDate, reservationEndDate) ||
        //            (start < reservationStartDate && start.AddDays(length) > reservationEndDate));
        //}

        //private bool IsWithinRange(DateTime dateCandidate, DateTime start, DateTime end)
        //{
        //    return (dateCandidate >= start && dateCandidate <= end);
        //}


    }
}
