using BookingApp.DTO;
using BookingApp.Domain.Models;
using BookingApp.Application.UseCases;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BookingApp.WPF.ViewModel.Tourist;

namespace BookingApp.View.Tourist
{
    public partial class TourGuestView : Window
    {
        public TourGuestView(TourDTO activeTour)
        {
            InitializeComponent();
            DataContext = new TourGuestViewModel(activeTour, DatesDataGrid, this);

        }
        private void DatesDataGrid_SelectionChanged_Handler(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext is TourGuestViewModel viewModel)
            {
                viewModel.DatesDataGrid_SelectionChanged(sender, e);
            }
        }


    }
}
