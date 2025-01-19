using BookingApp.Application.UseCases;
using BookingApp.Commands;
using BookingApp.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModel
{
    public class MonthStatsViewModel
    {
        public AccommodationDTO Acc { get; set; }
        public AccommodationStatsDTO stat1 { get; set; }
        private AccommodationStatsService service;
        private int year;
        public MonthStatsViewModel(AccommodationDTO acc,int year)
        {
            this.year = year;
            Acc = acc;
            stat1 = new AccommodationStatsDTO();
            service = new AccommodationStatsService();
            Update();
        }
        private void Update()
        {

            stat1.Reservations = service.GetReservationCountForAccommodationMonth(Acc.Id, year,1);
            stat1.Cancellations = service.GetCancellationCountForAccommodationMonth(Acc.Id, year, 1);
            stat1.Reschedulings = 2;
            stat1.RenovationRecommendations = 3;
            
        }
    }
}
