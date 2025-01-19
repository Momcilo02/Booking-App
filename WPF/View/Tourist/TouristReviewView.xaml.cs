using BookingApp.Commands;
using BookingApp.Domain.Models;
using BookingApp.DTO;
using BookingApp.Repository;
using BookingApp.Application.UseCases;
using BookingApp.WPF.ViewModel.Tourist;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace BookingApp.View.Tourist
{
    public partial class TouristReviewView : Page
    {

        public TouristReviewView(TourReservationDTO tourReservation, User user, Frame frame)
        {
            InitializeComponent();
            DataContext = new TouristReviewViewModel(tourReservation, user, frame, Review, tbxImageUrls, GuideKnowledgeStackPanel, GuideLanguageStackPanel, TourInterestStackPanel);
        }

    }
}
