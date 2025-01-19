using BookingApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTO.TourDTOs
{
    public class EndedTourDTO : INotifyPropertyChanged
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


        private string _tourImage;
        public string TourImage
        {
            get { return _tourImage; }
            set
            {
                if (value != _tourImage)
                {
                    _tourImage = value;
                    OnPropertyChanged(nameof(TourImage));
                }
            }
        }


        private string _location;
        public string Location 
        {
            get { return _location; }
            set
            {
                if (value != _location)
                {
                    _location = value;
                    OnPropertyChanged(nameof(Location));
                }
            }
        }


        private DateTime _tourDate;
        public DateTime TourDate
        {
            get { return _tourDate; }
            set
            {
                if (value != _tourDate)
                {
                    _tourDate = value;
                    OnPropertyChanged(nameof(TourDate));
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

        public EndedTourDTO()
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
