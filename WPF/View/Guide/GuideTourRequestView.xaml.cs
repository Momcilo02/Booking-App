using BookingApp.Domain.Models;
using BookingApp.DTO;
using BookingApp.View.Guide;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingApp.WPF.View.Guide
{
    /// <summary>
    /// Interaction logic for GuideTourRequestView.xaml
    /// </summary>
    public partial class GuideTourRequestView : Page
    {
        public Frame MainNavigationFrame { get; set; }
        public User User { get; set; }

        public bool ForComplex { get; set; }
        public GuideTourRequestView(Frame mainNavigationFrame, TourRequestFilterOptions filterOptions, User user, bool forComplex)
        {
            InitializeComponent();
            User = user;
            ForComplex = forComplex;
            DataContext = new GuideTourRequestViewModel(mainNavigationFrame,filterOptions, user,forComplex);
            MainNavigationFrame = mainNavigationFrame;
        }

        private void DropdownButton_Click(object sender, RoutedEventArgs e)
        {
            MainNavigationFrame.Navigate(new FilterTourRequests(MainNavigationFrame, User,ForComplex));
        }

    }
}
