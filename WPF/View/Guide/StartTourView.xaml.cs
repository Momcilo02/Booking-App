using BookingApp.Domain.Models;
using BookingApp.WPF.View.Guide;
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
    /// Interaction logic for StartTourView.xaml
    /// </summary>
    public partial class StartTourView : Page
    {

        public User User { get; set; }

        public StartTourView(User user)
        {
            InitializeComponent();
            var viewModel = new StartTourViewModel(user);
            DataContext = viewModel;
            User = user;

            var toursToday = new ToursToday();
            StartPageFrame.Navigate(toursToday);
            toursToday.DataContext = DataContext;
            viewModel.RequestNavigate += OnNavigateRequested;


        }

        private void OnNavigateRequested(UserControl control)
        {
            StartPageFrame.Content = control;
        }
    }
}
