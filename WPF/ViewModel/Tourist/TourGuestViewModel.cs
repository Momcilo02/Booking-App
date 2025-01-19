using BookingApp.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using BookingApp.Commands;
using BookingApp.Domain.Models;
using BookingApp.Application.UseCases;

namespace BookingApp.WPF.ViewModel.Tourist
{
    public class TourGuestViewModel
    {
        private TourGuestService tourGuestService { get; set; }
        public ObservableCollection<TourGuestDTO> SelectedTourGuests { get; set; }
        public TourDTO ActiveTour { get; set; }
        public ObservableCollection<TourGuestDTO> TourGuests { get; set; }
        public ICommand JoinTourClick { get; }
        private DataGrid _datesDataGrid;
        private readonly Window _window;
        public TourGuestViewModel(TourDTO activeTour, DataGrid datesDataGrid, Window window)
        {
            SelectedTourGuests = new ObservableCollection<TourGuestDTO>();
            ActiveTour = activeTour;
            TourGuests = new ObservableCollection<TourGuestDTO>();
            tourGuestService = new TourGuestService();
            JoinTourClick = new RelayCommand(param => JoinTour(param));
            _datesDataGrid = datesDataGrid;
            _window = window;
            LoadTourGuest();
        }


        public void LoadTourGuest()
        {
            foreach (TourGuest tourGuest in tourGuestService.GetAll())
            {
                if (tourGuest.Confirmation == false && ActiveTour.Id == tourGuest.TourReservationId)
                {
                    TourGuests.Add(new TourGuestDTO(tourGuest));
                }
            }
        }

        private void JoinTour(object parameter)
        {
            if (SelectedTourGuests.Count == 0)
            {
                MessageBox.Show("Please select at least one guest to join.");
                return;
            }

            foreach (var guest in SelectedTourGuests)
            {

                guest.Confirmation = true;
                tourGuestService.UpdateConfirmation(guest.Id, true);

            }
            MessageBox.Show("Request has been sent to the guide.");
            _window.Close();
        }
        public void DatesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedTourGuests.Clear();
            foreach (TourGuestDTO guest in _datesDataGrid.SelectedItems)
            {
                SelectedTourGuests.Add(guest);
            }
        }


    }
}
