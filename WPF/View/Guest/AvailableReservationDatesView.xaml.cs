using BookingApp.Domain.Models;
using BookingApp.DTO;
using BookingApp.WPF.ViewModel;
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

namespace BookingApp.View.Guest
{
    /// <summary>
    /// Interaction logic for AvailableReservationDatesView.xaml
    /// </summary>
    public partial class AvailableReservationDatesView : Window
    {

        public AvailableReservationDatesView(string message, List<DateOnly> startDates, List<DateOnly> endDates, User user, Accommodation accommodation, string numberOfPeople)
        {
            InitializeComponent();
            AvailableReservationDatesViewModel viewModel = new AvailableReservationDatesViewModel();
            viewModel.Initialize(user, startDates, endDates, accommodation, numberOfPeople);
            DataContext = viewModel;
        }
        
    }
}
