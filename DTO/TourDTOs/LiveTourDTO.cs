using BookingApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTO.TourDTOs
{
    public class LiveTourDTO : INotifyPropertyChanged
    {
        private int _id;
        public int Id
        {
            get { return _id; }
            set
            {
                if (value != _id)
                {
                    _id = value;
                    OnPropertyChanged(nameof(Id));
                }
            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (value != _name)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        public int _isFinished;
        public int IsFinished
        {
            get { return _isFinished; }
            set
            {
                if (value != _isFinished)
                {
                    _isFinished = value;
                    OnPropertyChanged(nameof(IsFinished));
                }
            }
        }

        public LiveTour ToLiveTour() 
        {
            return new LiveTour
            {
                Id = Id,
                TourId = TourId,
                TourTimeId = TourTimeId,
                IsFinished = IsFinished
            };
        }
        public int TourId { get; set; }
        public int TourTimeId { get; set; }


        private List<TourGuestDTO> _tourGuestDtos;
        public List<TourGuestDTO> TourGuestDtos
        {
            get { return _tourGuestDtos; }
            set
            {
                if (value != _tourGuestDtos)
                {
                    _tourGuestDtos = value;
                    OnPropertyChanged(nameof(TourGuestDtos));
                }
            }
        }

        private List<CheckPointDTO> _checkPointDtos;
        public List<CheckPointDTO> CheckPointDtos
        {
            get { return _checkPointDtos; }
            set
            {
                if (value != _checkPointDtos)
                {
                    _checkPointDtos = value;
                    OnPropertyChanged(nameof(CheckPointDtos));
                }
            }
        }

        public LiveTourDTO() 
        {
            CheckPointDtos = new List<CheckPointDTO>();
            TourGuestDtos = new List<TourGuestDTO>();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
