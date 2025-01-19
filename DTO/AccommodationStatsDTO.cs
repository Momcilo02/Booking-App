using BookingApp.Domain.Models;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTO
{
    public class AccommodationStatsDTO : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        }

        public int Id { get; set; }

        private int reservations;
        public int Reservations
        {
            get { return reservations; }
            set
            {
                if (value != reservations)
                {
                    reservations = value;
                    OnPropertyChanged(nameof(Reservations));
                }
            }
        }

        private int cancellations;
        public int Cancellations
        {
            get { return cancellations; }
            set
            {
                if (value != cancellations)
                {
                    cancellations = value;
                    OnPropertyChanged(nameof(Cancellations));
                }
            }
        }

        private int reschedulings;
        public int Reschedulings
        {
            get { return reschedulings; }
            set
            {
                if (value != reschedulings)
                {
                    reschedulings = value;
                    OnPropertyChanged(nameof(Reschedulings));
                }
            }
        }

        private int renovationRecommendations;
        public int RenovationRecommendations
        {
            get { return renovationRecommendations; }
            set
            {
                if (value != renovationRecommendations)
                {
                    renovationRecommendations = value;
                    OnPropertyChanged(nameof(RenovationRecommendations));
                }
            }
        }
        public AccommodationStatsDTO()
        {
            
        }


        public AccommodationStatsDTO(AccommodationStats stats)
        {
            Id = stats.Id;
            Reservations = stats.Reservations;
            Cancellations = stats.Cancellations;
            Reschedulings = stats.Reschedulings;
            RenovationRecommendations = stats.RenovationRecommendations;
        }
        public AccommodationStats toAccommodationStats()
        {
            return new AccommodationStats(Id, Reservations, Cancellations, Reschedulings, RenovationRecommendations);
        }
    }
}
