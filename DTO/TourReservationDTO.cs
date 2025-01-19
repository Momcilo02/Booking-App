using BookingApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTO
{
    public class TourReservationDTO:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public int Id { get; set; }
        private int userId;
        public int UserId
        {
            get { return userId; }
            set
            {
                if (value != userId)
                {
                    userId = value;
                    OnPropertyChanged(nameof(UserId));
                }
            }
        }

        private int _liveTourId;
        public int LiveTourId
        {
            get { return _liveTourId; }
            set
            {
                if (value != _liveTourId)
                {
                    _liveTourId = value;
                    OnPropertyChanged(nameof(LiveTourId));
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

        private int tourTimeId;
        public int TourTimeId
        {
            get { return tourTimeId; }
            set
            {
                if (value != tourTimeId)
                {
                    tourTimeId = value;
                    OnPropertyChanged(nameof(TourTimeId));
                }
            }
        }

        private int checkPointId;
        public int CheckPointId

        {
            get { return checkPointId; }
            set
            {
                if (value != checkPointId)
                {
                    checkPointId = value;
                    OnPropertyChanged(nameof(CheckPointId));
                }
            }
        }

        private int isActive;
        public int IsActive
        {
            get { return isActive; }
            set
            {
                if (value != isActive)
                {
                    isActive = value;
                    OnPropertyChanged(nameof(IsActive));
                }
            }
        }
       
        public TourReservationDTO()
        {

        }
        public TourReservationDTO(TourReservation tourReservation)
        {
            Id = tourReservation.Id;
            UserId = tourReservation.UserId;
            TourId= tourReservation.TourId;
            TourTimeId = tourReservation.TourTimeId;
            CheckPointId = tourReservation.CheckPointId;
            IsActive = tourReservation.IsActive;
        }
        public TourReservation ToTourReservation()
        {
            return new TourReservation(Id, userId,tourId,tourTimeId, checkPointId, isActive,-1);
        }
        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
