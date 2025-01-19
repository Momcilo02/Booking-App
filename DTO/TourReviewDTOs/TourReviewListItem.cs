using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTO.TourReviewDTOs
{
    public class TourReviewListItem : INotifyPropertyChanged
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

        private string _checkPoint;
        public string CheckPoint
        {
            get { return _checkPoint; }
            set
            {
                if (value != _checkPoint)
                {
                    _checkPoint = value;
                    OnPropertyChanged(nameof(CheckPoint));
                }
            }
        }

        private int _guideKnowledge;
        public int GuideKnowledge
        {
            get { return _guideKnowledge; }
            set
            {
                if (value != _guideKnowledge)
                {
                    _guideKnowledge = value;
                    OnPropertyChanged(nameof(GuideKnowledge));
                }
            }
        }

        private int _guideLanguage;
        public int GuideLanguage
        {
            get { return _guideLanguage; }
            set
            {
                if (value != _guideLanguage)
                {
                    _guideLanguage = value;
                    OnPropertyChanged(nameof(GuideLanguage));
                }
            }
        }
        private int _tourInterest;
        public int TourInterest
        {
            get { return _tourInterest; }
            set
            {
                if (value != _tourInterest)
                {
                    _tourInterest = value;
                    OnPropertyChanged(nameof(TourInterest));
                }
            }
        }

        private string _comment;
        public string Comment
        {
            get { return _comment; }
            set
            {
                if (value != _comment)
                {
                    _comment = value;
                    OnPropertyChanged(nameof(Comment));
                }
            }
        }

        private bool _isValid;
        public bool IsValid
        {
            get { return _isValid; }
            set
            {
                if (value != _isValid)
                {
                    _isValid = value;
                    OnPropertyChanged(nameof(IsValid));
                }
            }
        }

        public string IsValidMessage => IsValid ? "The review is valid" : "The review is not valid";
        public TourReviewListItem()
        {

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
