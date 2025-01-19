using BookingApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTO
{
    public class AccommodationRenovationDTO : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public int Id { get; set; }

        private int accommodationId;
        public int AccommodationId
        {
            get { return accommodationId; }
            set
            {
                if (value != accommodationId)
                {
                    accommodationId = value;
                    OnPropertyChanged(nameof(AccommodationId));
                }
            }
        }
        private DateTime firstDay;
        public DateTime FirstDay
        {
            get { return firstDay; }
            set
            {
                if (firstDay != value)
                {
                    firstDay = value;
                    OnPropertyChanged(nameof(FirstDay));
                }
            }
        }


        private DateTime lastDay;
        public DateTime LastDay
        {
            get { return lastDay; }
            set
            {
                if (lastDay != value)
                {
                    lastDay = value;
                    OnPropertyChanged(nameof(LastDay));
                }
            }
        }
        private string comment;
        public string Comment
        {
            get { return comment; }
            set
            {
                if (value != comment)
                {
                    comment = value;
                    OnPropertyChanged(nameof(Comment));
                }
            }
        }
        public AccommodationRenovationDTO(AccommodationRenovation renovation)
        { 
            Id = renovation.Id;
            AccommodationId = renovation.AccommodationId;
            FirstDay = renovation.FirstDay;
            LastDay = renovation.LastDay;

        }
        public AccommodationRenovationDTO()
        {
            
        }
       
        public AccommodationRenovation toAccommodationRenovation()
        {
            return new AccommodationRenovation(Id,AccommodationId, FirstDay, LastDay, Comment);
        }

        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        }
    }
}
