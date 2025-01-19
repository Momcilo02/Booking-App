using BookingApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTO
{
    public class TourTimeDTO : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public int Id { get; set; }
        private DateTime? time;
        public DateTime? Time
        {
            get { return time; }
            set
            {
                if (value != time)
                {
                    time = value;
                    OnPropertyChanged(nameof(Time));
                }
            }
        }
        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
                    OnPropertyChanged(nameof(IsSelected));
                }
            }
        }
        private int maxGuests;
        public int MaxGuests
        {
            get { return maxGuests; }
            set
            {
                if (value != maxGuests)
                {
                    maxGuests = value;
                    OnPropertyChanged(nameof(maxGuests));
                }
            }
        }

        private int tourId;
        public int TourId
        {
            get { return tourId; }
            set
            {
                if (value != tourId)
                {
                    tourId = value;
                    OnPropertyChanged(nameof(TourId));
                }
            }
        }

        private bool started;
        public bool Started
        {
            get { return started; }
            set
            {
                if (value != started)
                {
                    started = value;
                    OnPropertyChanged(nameof(Started));
                }
            }
        }


        public TourTimeDTO()
        {

        }
        public TourTimeDTO(TourTime tourTime)
        {
            Id = tourTime.Id;
            Time = tourTime.Time;
            MaxGuests = tourTime.MaxGuests;
            TourId = tourTime.TourId;
            Started = tourTime.Started;
        }

        public TourTime? ToTourTimeV2()
        {
            if (Time == null) return null;

            return new TourTime(Id, Time.Value, maxGuests, tourId, started);
        }
        public TourTime? ToTourTime()
        {
            if (Time == null) return null;

            return new TourTime(Id, Time.Value, maxGuests, tourId, started);
        }
        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        }
    }
}
