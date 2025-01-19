using BookingApp.Domain.Models;
using BookingApp.WPF.View.Guide;
using BookingApp.WPF.ViewModel.Guide;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace BookingApp.View.Guide
{
    /// <summary>
    /// Interaction logic for GuideMainView.xaml
    /// </summary>
    public partial class GuideMainView : Window
    {
        public User User { get; set; }

        public GuideMainView(User user)
        {
            InitializeComponent();
            DataContext = new GuideMainViewModel(user);
            User = user;
            var guideHomePage = new GuideHomePage();
            guideHomePage.DataContext = DataContext;

            MainNavigationFrame.Navigate(guideHomePage);
        }

  

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (MainNavigationFrame.CanGoBack)
            {
                MainNavigationFrame.GoBack();
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var createTourPage = new CreateTourView(User, MainNavigationFrame, null);
            MainNavigationFrame.Navigate(createTourPage);
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            var guideHomePage = new GuideHomePage();
            MainNavigationFrame.Navigate(guideHomePage);
            guideHomePage.DataContext = DataContext;
        }

        private void Start_Tour(object sender, RoutedEventArgs e)
        {
            MainNavigationFrame.Navigate(new StartTourView(User));
        }

        private void Ended_Tours(object sender, RoutedEventArgs e)
        {
            EndedToursView endedTours = new EndedToursView(User);
            endedTours.ShowDialog();
        }

        private void CreateTour_Click(object sender, RoutedEventArgs e)
        {
            MainNavigationFrame.Navigate(new CreateTourView(User, MainNavigationFrame, null));
        }

        private void NewWindow_Closed(object sender, EventArgs e)
        {
            var viewModel = DataContext as GuideMainViewModel;
            viewModel?.RefreshTours();
        }

        private void Statistics_Click(object sender, RoutedEventArgs e)
        {
            MainNavigationFrame.Navigate(new TourStatisticsView(User));
        }

        private void Tour_Requests(object sender, RoutedEventArgs e)
        {
            MainNavigationFrame.Navigate(new GuideTourRequestView(MainNavigationFrame, null, User, false));
        }

        private void Complex_Tour_Requests(object sender, RoutedEventArgs e)
        {
            MainNavigationFrame.Navigate(new GuideTourRequestView(MainNavigationFrame, null, User, true));
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            var guideHomePage = new GuideHomePage();
            MainNavigationFrame.Navigate(guideHomePage);
            guideHomePage.DataContext = DataContext;
        }

        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            MainNavigationFrame.Navigate(new QuitConfirmationView(User, MainNavigationFrame));
        }
    }
}
