using BookingApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace BookingApp.DTO
{
    public class TourRequestDTO : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public int Id { get; set; }
        public int TouristId { get; set; }
        public int GuideId { get; set; }

        private Location location;
        public Location Location
        {
            get { return location; }
            set
            {
                if (value != location)
                {
                    location = value;
                    OnPropertyChanged(nameof(Location));
                }
            }
        }
        private string description;
        public string Description
        {
            get { return description; }
            set
            {
                if (value != description)
                {
                    description = value;
                    OnPropertyChanged(nameof(Description));
                }
            }
        }

        private Language language;
        public Language Language
        {
            get { return language; }
            set
            {
                if (value != language)
                {
                    language = value;
                    OnPropertyChanged(nameof(Language));
                }
            }
        }

        private DateOnly startDate;
        public DateOnly StartDate
        {
            get { return startDate; }
            set
            {
                if (value != startDate)
                {
                    startDate = value;
                    OnPropertyChanged(nameof(StartDate));
                }
            }
        }

        private DateOnly endDate;
        public DateOnly EndDate
        {
            get { return endDate; }
            set
            {
                if (value != endDate)
                {
                    endDate = value;
                    OnPropertyChanged(nameof(EndDate));
                }
            }
        }
        private int status;
        public int Status
        {
            get { return status; }
            set
            {
                if (value != status)
                {
                    status = value;
                    OnPropertyChanged(nameof(Status));
                }
            }
        }
        private int complexTourId;
        public int ComplexTourId
        {
            get { return complexTourId; }
            set
            {
                if (value != complexTourId)
                {
                    complexTourId = value;
                    OnPropertyChanged(nameof(ComplexTourId));
                }
            }
        }
        private int guestNumber;
        public int GuestNumber
        {
            get { return guestNumber; }
            set
            {
                if (value != guestNumber)
                {
                    guestNumber = value;
                    OnPropertyChanged(nameof(GuestNumber));
                }
            }
        }
        private DateOnly finalDate;
        public DateOnly FinalDate
        {
            get { return finalDate; }
            set
            {
                if (value != finalDate)
                {
                    finalDate = value;
                    OnPropertyChanged(nameof(FinalDate));
                }
            }
        }
        private string statustxt;
        public string Statustxt
        {
            get { return statustxt; }
            set
            {
                if (value != statustxt)
                {
                    statustxt = value;
                    OnPropertyChanged(nameof(Statustxt));
                }
            }
        }
        public TourRequestDTO() { }
        public TourRequestDTO(TourRequest tour)
        {
            Id = tour.Id;
            TouristId = tour.TouristId;
            Location = tour.Location;
            Description = tour.Description;
            Language = tour.Language;
            StartDate = tour.StartDate;
            EndDate = tour.EndDate;
            Status = tour.Status;
            GuestNumber = tour.GuestNumber;
            FinalDate = tour.FinalDate;
            ComplexTourId = tour.ComplexTourId;
            if (Status == 0)
            {
                Statustxt = "On wait";
            }
            else if (Status == 1)
            {
                Statustxt = "Accepted";
            }
            else
            {
                Statustxt = "Invalid";
            }


        }
        public TourRequest ToTourRequest()
        {
            return new TourRequest(Id, TouristId, location, description, language, startDate, endDate, status, guestNumber, finalDate, GuideId);
        }
        public TourRequest ToComplexTourRequest()
        {
            return new TourRequest(Id, TouristId, location, description, language, startDate, endDate, status, guestNumber, finalDate, GuideId, complexTourId);
        }
        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        }
    }
}
