using BookingApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTO
{
    public class TouristReviewDTO:INotifyPropertyChanged
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

        private int guideKnowledge;
        public int GuideKnowledge
        {
            get { return guideKnowledge; }
            set
            {
                if (value != guideKnowledge)
                {
                    guideKnowledge = value;
                    OnPropertyChanged(nameof(GuideKnowledge));
                }
            }
        }

        private int guideLanguage;
        public int GuideLanguage
        {
            get { return guideLanguage; }
            set
            {
                if (value != guideLanguage)
                {
                    guideLanguage = value;
                    OnPropertyChanged(nameof(guideLanguage));
                }
            }
        }

 	private int _tourReservationId;
        public int TourReservationId
        {
            get { return _tourReservationId; }
            set
            {
                if (value != _tourReservationId)
                {
                    _tourReservationId = value;
                    OnPropertyChanged(nameof(TourReservationId));
                }
            }
        }

        private int tourInterest;
        public int TourInterest
        {
            get { return tourInterest; }
            set
            {
                if (value != tourInterest)
                {
                    tourInterest = value;
                    OnPropertyChanged(nameof(TourInterest));
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

        private int isValid;
        public int IsValid
        {
            get { return isValid; }
            set
            {
                if (value != isValid)
                {
                    isValid = value;
                    OnPropertyChanged(nameof(IsValid));
                }
            }
        }

        public TouristReviewDTO()
        {

        }
        public TouristReviewDTO(TouristReview touristReview)
        {
            Id = touristReview.Id;
            UserId = touristReview.UserId;
            GuideKnowledge = touristReview.GuideKnowledge;
            GuideLanguage = touristReview.GuideLanguage;
            TourInterest = touristReview.TourInterest;
            Comment = touristReview.Comment;
        }
        public TouristReview ToTouristReview()
        {
            return new TouristReview(Id, userId, guideKnowledge, guideLanguage, tourInterest, comment);
        }
        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        }
    }
}
