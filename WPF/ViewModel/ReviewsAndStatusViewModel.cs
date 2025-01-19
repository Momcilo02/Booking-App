using BookingApp.Application.UseCases;
using BookingApp.Domain.Models;
using BookingApp.DTO;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.WPF.ViewModel
{
    public class ReviewsAndStatusViewModel
    {
        private User _user;
        public ObservableCollection<AccommodationReviewDTO> _reviws { get; set; }
        private GuestReviewService _guestReviewService;
        public ObservableCollection<AccommodationReviewDTO> _reviwsToShow { get; set; }
        private AccommodationReviewService _reviewService;
        private UserRepository _userRep;
        public ReviewsAndStatusViewModel(User user)
        {
            _userRep = new UserRepository();
            _reviewService = new AccommodationReviewService();
            _reviws = new ObservableCollection<AccommodationReviewDTO>();
            _reviwsToShow = new ObservableCollection<AccommodationReviewDTO>();
            _guestReviewService = new GuestReviewService();
            _user = user;
        }
        
    }
}
