using BookingApp.Application.UseCases;
using BookingApp.Commands;
using BookingApp.DTO;
using BookingApp.WPF.View.Owner;
using LiveCharts.Wpf.Charts.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModel
{
    public class AccommodationStatsViewModel
    {
        public AccommodationDTO Acc { get; set; }
        public AccommodationStatsDTO stat1 { get; set; }
        public AccommodationStatsDTO stat2 { get; set; }
        private AccommodationStatsService service;
        public ICommand NavigationClick { get; }
        public ICommand NavigationClick1 { get; }
        public AccommodationStatsViewModel(AccommodationDTO acc)
        {
            Acc = acc;
            stat1 = new AccommodationStatsDTO();
            service = new AccommodationStatsService();
            stat2 = new AccommodationStatsDTO();
            NavigationClick = new RelayCommand(param => Navigation(param));
            NavigationClick1 = new RelayCommand(param => Navigation1(param));
            Update();
        }
        private void Navigation(object parameter)
        {
            MonthStats window = new MonthStats(Acc,2020);
            window.ShowDialog();
        }
        private void Navigation1(object parameter)
        {
            MonthStats window = new MonthStats(Acc, 2021);
            window.ShowDialog();
        }
        private void Update()
        {

            stat1.Reservations = service.GetReservationCountForAccommodationYear(Acc.Id, 2020);
            stat1.Cancellations = service.GetCancellationCountForAccommodationYear(Acc.Id, 2020);
            stat1.Reschedulings = 2;
            stat1.RenovationRecommendations = 3;
            stat2.Reservations = service.GetReservationCountForAccommodationYear(Acc.Id, 2021);
            stat2.Cancellations = service.GetCancellationCountForAccommodationYear(Acc.Id, 2021);
            stat2.Reschedulings = 0;
            stat2.RenovationRecommendations = 1;
        }
    }
}
