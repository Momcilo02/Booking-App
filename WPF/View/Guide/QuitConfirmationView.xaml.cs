﻿using BookingApp.Domain.Models;
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
    /// Interaction logic for QuitConfirmationView.xaml
    /// </summary>
    public partial class QuitConfirmationView : Page
    {
        public QuitConfirmationView(User user, Frame mainFrame)
        {
            InitializeComponent();
            DataContext = new QuitConfirmationViewModel(user,mainFrame);
        }
    }
}
