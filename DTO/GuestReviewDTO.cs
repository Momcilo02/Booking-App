using BookingApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTO
{
    public class GuestReviewDTO : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public int Id { get; set; }

        private int cleaness;
        public int Cleaness
        {
            get { return cleaness; }
            set
            {
                if (value != cleaness)
                {
                    cleaness = value;
                    OnPropertyChanged(nameof(Cleaness));
                }
            }
        }
        private AccommodationReservation accommodationReservation;
        public AccommodationReservation AccommodationReservation
        {
            get { return accommodationReservation; }
            set
            {
                if (value != accommodationReservation)
                {
                    accommodationReservation = value;
                    OnPropertyChanged(nameof(AccommodationReservation));
                }
            }
        }
        private int correctness;
        public int Correctness
        {
            get { return correctness; }
            set
            {
                if (value != correctness)
                {
                    correctness = value;
                    OnPropertyChanged(nameof(Correctness));
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
        public GuestReviewDTO()
        {
            
        }
        public GuestReviewDTO(GuestReview review)
        {
            Id = review.Id;
            Cleaness = review.Cleaness;
            Comment = review.Comment;
            Correctness = review.Correctness;
            AccommodationReservation = review.AccommodationReservation;
        }
        public GuestReview ToGuestReview()
        {
            return new GuestReview(Id,AccommodationReservation,Cleaness,Correctness,Comment);
        }
        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        }
    }
}
