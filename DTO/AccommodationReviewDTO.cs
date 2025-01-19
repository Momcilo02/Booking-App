using BookingApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTO
{
    public class AccommodationReviewDTO : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public int Id { get; set; }
        private User guest;
        public User Guest
        {
            get { return guest; }
            set
            {
                if(value != guest)
                {
                    guest = value;
                    OnPropertyChanged(nameof(Guest));
                }
            }
        }
        private Accommodation accommodation;
        public Accommodation Accommodation
        {
            get { return accommodation; }
            set
            {
                if(value != accommodation)
                {
                    accommodation = value;
                    OnPropertyChanged(nameof(Accommodation));
                }
            }
        }
        private int cleanness;
        public int Cleanness
        {
            get { return cleanness; }
            set
            {
                if (value != cleanness)
                {
                    cleanness = value;
                    OnPropertyChanged(nameof(Cleanness));
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
        private List<Image> images;
        public List<Image> Images
        {
            get { return images; }
            set
            {
                if(images!= value)
                {
                    images = value;
                    OnPropertyChanged(nameof(Images));
                }
            }
        }
        public AccommodationReviewDTO()
        {
            
        }
        public AccommodationReviewDTO(AccommodationReview review)
        {
            Id = review.Id;
            Guest = review.Guest;
            Accommodation = review.Accommodation;
            Cleanness = review.Cleanness;
            Correctness = review.Correctness;
            Comment = review.Comment;
            Images = review.Images;
        }
        public AccommodationReview ToAccommodationReview()
        {
            return new AccommodationReview(Id, Guest, Accommodation, Cleanness, Correctness, Comment, Images);
        }

        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        }



    }
}
