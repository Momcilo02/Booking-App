using BookingApp.DTO;
using BookingApp.Domain.Models;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
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

namespace BookingApp.View.Owner
{
    /// <summary>
    /// Interaction logic for AccommodationReservationView.xaml
    /// </summary>
    public partial class AccommodationReservationView : Window
    {
        public AccommodationReservationDTO accommodationReservation { get; set; }
        public GuestReviewDTO guestReview { get; set; }
        private GuestReviewRepository guestReviewRepository { get; set; }
        public AccommodationReservationView(AccommodationReservationDTO accommodationReservationDTO)
        {
            accommodationReservation = accommodationReservationDTO;
            guestReview = new GuestReviewDTO();
            guestReview.AccommodationReservation = accommodationReservation.ToAccommodationReservation();
            guestReviewRepository = new GuestReviewRepository();
            InitializeComponent();
            DataContext = this;
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            guestReviewRepository.Save(guestReview.ToGuestReview());
            this.Close();
        }
        //private void ChangeSelection(object sender, SelectionChangedEventArgs e)
        //{
        //    if (cboCleaness.SelectedItem != null)
        //    {
        //        ComboBoxItem selectedItem = (ComboBoxItem)cboCleaness.SelectedItem;
        //        guestReview.Cleaness = int.Parse(selectedItem.Content.ToString());
        //    }
        //}
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            var radioButton = sender as RadioButton;
            if (radioButton != null && radioButton.IsChecked == true)
            {
                int cleaness;
                if (int.TryParse(radioButton.Content.ToString(), out cleaness))
                {
                    guestReview.Cleaness = cleaness;
                }
            }
        }
        private void RadioButton1_Checked(object sender, RoutedEventArgs e)
        {
            var radioButton = sender as RadioButton;
            if (radioButton != null && radioButton.IsChecked == true)
            {
                int cleaness;
                if (int.TryParse(radioButton.Content.ToString(), out cleaness))
                {
                    guestReview.Correctness = cleaness;
                }
            }
        }
        //private void cboCorrectness_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if (cboCorrectness.SelectedItem != null)
        //    {
        //        ComboBoxItem selectedItem = (ComboBoxItem)cboCorrectness.SelectedItem;
        //        guestReview.Correctness = int.Parse(selectedItem.Content.ToString());
        //    }
        //}

        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
