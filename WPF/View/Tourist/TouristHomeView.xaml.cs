using BookingApp.Domain.Models;
using BookingApp.WPF.ViewModel.Tourist;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace BookingApp.WPF.View.Tourist
{
    /// <summary>
    /// Interaction logic for TouristHomeView.xaml
    /// </summary>
    public partial class TouristHomeView : Page
    {
        public TouristHomeView(User user, Frame frame, Frame frame2)
        {
            InitializeComponent();
            DataContext = new TouristHomeViewModel(user, frame, frame2);
        }


    }
}
