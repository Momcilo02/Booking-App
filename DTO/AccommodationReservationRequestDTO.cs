using BookingApp.Domain.Models;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace BookingApp.DTO
{
    public class AccommodationReservationRequestDTO : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public int Id { get; set; }
        public User Owner { get; set; }
        private AccommodationReservation oldReservation;
        public AccommodationReservation OldReservation
        {
            get { return oldReservation; }
            set
            {
                if (value != oldReservation)
                {
                    oldReservation = value;
                    OnPropertyChanged(nameof(OldReservation));
                }
            }
        }
        private string ownerUsername;
        public string OwnerUsername
        {
            get { return ownerUsername; }
            set
            {
                if(ownerUsername != value)
                {
                    ownerUsername = value;
                    OnPropertyChanged(nameof(OwnerUsername));
                }
            }
        }
        private string newStartDate;
        public string NewStartDate
        {
            get { return newStartDate; }
            set
            {
                if (newStartDate != value)
                {
                    newStartDate = value;
                    OnPropertyChanged(nameof(NewStartDate));
                }
            }
        }

        private string newEndDate;
        public string NewEndDate
        {
            get { return newEndDate; }
            set
            {
                if (newEndDate != value)
                {
                    newEndDate = value;
                    OnPropertyChanged(nameof(NewEndDate));
                }
            }
        }
        private Enumeration.AccommodationReservationRquest status;
        public Enumeration.AccommodationReservationRquest Status
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
        private string comment;
        public string Comment
        {
            get { return comment; }
            set
            {
                if (comment != value)
                {
                    comment = value;
                    OnPropertyChanged(nameof(Comment));
                }
            }
        }
        public string StatusImage { get; set; }
        public AccommodationReservationRequestDTO()
        {
            
        }
        public AccommodationReservationRequestDTO(AccommodationReservationRequest accommodationReservationRequest)
        {
            Id = accommodationReservationRequest.Id;
            OldReservation = accommodationReservationRequest.OldReservation;
            NewStartDate = accommodationReservationRequest.NewStartDate.ToString();
            NewEndDate = accommodationReservationRequest.NewEndDate.ToString();
            Comment = accommodationReservationRequest.Comment;
            Status = accommodationReservationRequest.Status;
            StatusImage = string.Empty;
            Owner = OldReservation.Accommodation.Owner;
            OwnerUsername = OldReservation.Accommodation.Owner.Username;
        }
        public AccommodationReservationRequest ToAccommodationReservationRequest()
        {
            DateOnly start = DateOnly.Parse(newStartDate);
            DateOnly end = DateOnly.Parse(newEndDate);
            return new AccommodationReservationRequest(Id, OldReservation, start,end,Status,Comment);
        }
        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
