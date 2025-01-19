using BookingApp.Application.UseCases;
using BookingApp.Commands;
using BookingApp.Domain.Models;
using BookingApp.DTO.TourReviewDTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModel.Guide
{
    public class TourReviewsViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<TourReviewListItem> _reviews;
        private TouristReviewService _touristReviewService;

        public int LiveTourId { get; set; }
        public ObservableCollection<TourReviewListItem> Reviews
        {
            get => _reviews;
            set
            {
                _reviews = value;
                OnPropertyChanged(nameof(Reviews));
            }
        }

        public ICommand ReportReviewCommand { get; }

        public TourReviewsViewModel(User user, int liveTourId)
        {
            _touristReviewService = new TouristReviewService();
            Reviews = new ObservableCollection<TourReviewListItem>();
            LiveTourId = liveTourId;

            var reviews = _touristReviewService.GetReviewsForEndedTour(liveTourId);
            reviews.ForEach(x => Reviews.Add(x));

            ReportReviewCommand = new RelayCommand(ReportReview);
        }

        private void ReportReview(object reviewId)
        {
            var reviewToReport = Reviews.FirstOrDefault(r => r.Id == (int)reviewId);
            if (reviewToReport != null)
            {
                _touristReviewService.ReportReview(reviewToReport.Id);
                var reviews = _touristReviewService.GetReviewsForEndedTour(LiveTourId);
                Reviews.Clear();
                reviews.ForEach(x => Reviews.Add(x));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
