using BookingApp.Domain.Models;
using BookingApp.DTO;
using BookingApp.Repository;
using BookingApp.Application.UseCases;
using BookingApp.WPF.ViewModel.Tourist;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using static BookingApp.WPF.ViewModel.Tourist.PeopleReportViewModel;
using static System.Net.Mime.MediaTypeNames;

namespace BookingApp.View.Tourist
{
    public partial class PeopleReportWindow : Page
    {
        public PeopleReportWindow(List<string> guests, TourDTO selectedTour, User user, System.Windows.Controls.Frame frame)
        {
            InitializeComponent();
            DataContext = new PeopleReportViewModel(guests, selectedTour, user, frame, TextBoxFirstName, TextBoxLastName, TextBoxAge, ValidationFirstName,
                ValidationLastName, ValidationAge, FinalMessage, Previous, Next);
        }

    }
}
