﻿using System;
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
using BookingApp.Domain.Models;
using BookingApp.WPF.ViewModel;

namespace BookingApp.View.Guest
{
    /// <summary>
    /// Interaction logic for ChangeReservationView.xaml
    /// </summary>
    public partial class ChangeReservationView : Window
    {
        public ChangeReservationView(User guest, AccommodationReservation reservation)
        {
            InitializeComponent();
            DataContext = new ChangeAccommodationReservationViewModel(guest, reservation, this);
        }
    }
}
