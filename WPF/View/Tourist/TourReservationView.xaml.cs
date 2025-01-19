using BookingApp.Domain.Models;
using BookingApp.DTO;
using BookingApp.Application.UseCases;
using BookingApp.WPF.ViewModel.Tourist;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using static BookingApp.WPF.ViewModel.Tourist.PeopleReportViewModel;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace BookingApp.View.Tourist
{
    public partial class TourReservationView : Page
    {
        public TourReservationView(TourDTO selectedTour, User user, Frame frame, Frame frame2)
        {
            InitializeComponent();
            DataContext = new TourReservationViewModel(selectedTour, user, frame, frame2, TextBoxGuests, ComboBoxVoucher, CallerWindow.TourReservationView, ErrorDate, ErrorGuests);
        }
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (DataContext is TourReservationViewModel viewModel && sender is CheckBox checkBox && checkBox.DataContext is TourTimeDTO tourTime)
            {
                viewModel.SelectedTime = tourTime;
                foreach (var time in viewModel.TourTimes)
                {
                    if (time != tourTime)
                    {
                        time.IsSelected = false;
                    }
                }
            }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (DataContext is TourReservationViewModel viewModel && sender is CheckBox checkBox && checkBox.DataContext is TourTimeDTO tourTime)
            {
                if (viewModel.SelectedTime == tourTime)
                {
                    viewModel.SelectedTime = null;
                }
            }
        }


    }
}
