using BookingApp.Domain.Models;
using BookingApp.WPF.ViewModel.Guide;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BookingApp.View.Guide
{
    /// <summary>
    /// Interaction logic for TourReviewsView.xaml
    /// </summary>
    public partial class TourReviewsView : Window
    {
        public TourReviewsViewModel TourReviewsViewModel { get; set; } 
        public TourReviewsView(User user,int liveTourId)
        {
            InitializeComponent();
            TourReviewsViewModel = new TourReviewsViewModel(user,liveTourId);
            DataContext = TourReviewsViewModel;
        }
    }
}
